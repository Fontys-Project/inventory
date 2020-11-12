using InventoryDAL.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IBuilderFactory builderFactory;
        private readonly IProductEntityDAO productEntityDAO;

        public ProductsRepository(IProductEntityDAO productEntityDAO,
                                  IBuilderFactory builderFactory)
        {
            this.productEntityDAO = productEntityDAO;
            this.builderFactory = builderFactory;
        }

        public List<Product> GetAll()
        {
            List<ProductEntity> productEntities = productEntityDAO.GetAll();
            return productEntities
                .Select(productEntity => BuildProduct(productEntity))
                .ToList();
        }

        public Product Get(int id)
        {
            ProductEntity productEntity = productEntityDAO.Get(id);
            return BuildProduct(productEntity);
        }

        public Product Add(Product product)
        {
            ProductEntity productEntity = BuildProductEntity(product);
            productEntity = productEntityDAO.Add(productEntity);
            return BuildProduct(productEntity);
        }

        public void Modify(Product product)
        {
            ProductEntity productEntity = BuildProductEntity(product);
            productEntityDAO.Modify(productEntity);
        }

        public void Remove(int id)
        {
            productEntityDAO.Remove(id);
        }

        private Product BuildProduct(ProductEntity productEntity)
        {
            var productBuilder = builderFactory.CreateProductBuilder(productEntity);
            //productBuilder.BuildTags();
            //productBuilder.BuildStocks();
            return productBuilder.GetResult();
        }

        private ProductEntity BuildProductEntity(Product product)
        {
            var productEntityBuilder = builderFactory.CreateProductEntityBuilder(product);
            //productEntityBuilder.BuildProductTagEntities();
            //productEntityBuilder.BuildStockEntities();
            return productEntityBuilder.GetResult();
        }
    }
}
