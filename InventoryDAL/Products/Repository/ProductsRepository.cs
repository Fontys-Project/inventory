using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IProductEntityDAO productEntityDAO;
        private readonly IProductConverter productConverter;

        public ProductsRepository(IProductEntityDAO productEntityDAO, IProductConverter productConverter)
        {
            this.productEntityDAO = productEntityDAO;
            this.productConverter = productConverter;
        }

        public void Add(Product product)
        {
            ProductEntity productEntity = productConverter.ConvertToNewProductEntity(product);
            productEntityDAO.Add(productEntity);
        }

        public List<Product> GetAll()
        {
            return productEntityDAO.GetAll()
                .Select(entity => productConverter.ConvertToProduct(entity)).ToList();
        }

        public Product Get(int id)
        {
            ProductEntity entity = productEntityDAO.Get(id);
            return productConverter.ConvertToProduct(entity);
        }

        public void Modify(Product product)
        {
            ProductEntity entity = productConverter.ConvertToExistingProductEntity(product);
            productEntityDAO.Modify(entity);
        }

        public void Remove(int id)
        {
            productEntityDAO.Remove(id);
        }
    }
}
