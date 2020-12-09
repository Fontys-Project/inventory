using InventoryDAL.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IConverterFactory converterFactory;
        private readonly IProductEntityDAO productEntityDAO;

        private readonly Dictionary<Product,IProductEntity> productCache;


        public ProductsRepository(IProductEntityDAO productEntityDAO,
                                  IConverterFactory converterFactory)
        {
            this.productEntityDAO = productEntityDAO;
            this.converterFactory = converterFactory;
            productCache = new Dictionary<Product, IProductEntity>();
        }

        // Handle cacheing of object on instantiation
        private void OnObjectCreation(Product product, IProductEntity productEntity)
        {
            productCache.Add(product, productEntity);
        }

        public List<Product> GetAll()
        {
            List<ProductEntity> productEntities = productEntityDAO.GetAll();
            
            // Trigger with where, only products not cached, and then select all uncached product entities to convert Products that will be added
            // to the cache with the OnObjectCreation delegate.
            productEntities.Where(productEntity => productCache.Values.Any(cacheEntity => cacheEntity.Id == productEntity.Id) == false)
                .Select(productEntity => converterFactory.productConverter.Convert(productEntity, OnObjectCreation));
            
            return productCache.Keys.ToList<Product>();
        }

        public Product Get(int id)
        {
            Product product = productCache.Keys.Where(p => p.Id == id).FirstOrDefault();
            if (product == null) {
                ProductEntity productEntity = productEntityDAO.Get(id);
                return converterFactory.productConverter.Convert(productEntity, OnObjectCreation);
            } else
            {
                return product;
            }
        }

        public Product Add(Product product)
        {
            ProductEntity productEntity = converterFactory.productEntityConverter.Convert(product);
            productEntity = productEntityDAO.Add(productEntity);
            return converterFactory.productConverter.Convert(productEntity, OnObjectCreation);
        }

        public void Modify(Product product)
        {
            ProductEntity productEntity = converterFactory.productEntityConverter.Convert(product);
            productEntityDAO.Modify(productEntity);
        }

        public void Remove(int id)
        {
            productEntityDAO.Remove(id);

            //TODO : cleanup childs.
        }

    }
}
