using System.Collections.Generic;
using System.Linq;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryLogic.Products;

namespace InventoryDAL.Products.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IBuilderFactory builderFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IProductEntityDAO productEntityDAO;

        public ProductsRepository(IDAOFactory daoFactory,
                                  IBuilderFactory builderFactory)
        {

            this.builderFactory = builderFactory; 
            this.daoFactory = daoFactory; 
            this.productEntityDAO = daoFactory.ProductEntityDAO;
        }

        public List<Product> GetAll()
        {
            List<ProductEntity> productEntities = productEntityDAO.GetAll();
            return productEntities
                .Select(productEntity => BuildProduct(productEntity, true))
                .ToList();
        }

        public Product Get(int id)
        {
            ProductEntity productEntity = productEntityDAO.Get(id);
            return BuildProduct(productEntity, true);
        }

        public Product Add(Product product)
        {
            ProductEntity productEntity = BuildProductEntity(product, false);
            productEntity = productEntityDAO.Add(productEntity);
            return BuildProduct(productEntity, true);
        }

        public void Modify(Product product)
        {
            ProductEntity productEntity = BuildProductEntity(product, false);
            productEntityDAO.Modify(productEntity);
        }

        public void Remove(int id)
        {
            productEntityDAO.Remove(id);
        }

        private Product BuildProduct(ProductEntity productEntity, bool includesNavigationProperties)
        {
            var productBuilder = builderFactory.CreateProductBuilder(productEntity);
            if (includesNavigationProperties)
            {
                productBuilder.BuildTags();
                productBuilder.BuildStocks();
            }
            return productBuilder.GetResult();
        }

        private ProductEntity BuildProductEntity(Product product, bool includesNavigationProperties)
        {
            var productEntityBuilder = builderFactory.CreateProductEntityBuilder(product);
            if (includesNavigationProperties)
            {
                productEntityBuilder.BuildProductTagEntities();
                productEntityBuilder.BuildStockEntities();
            }
            return productEntityBuilder.GetResult();
        }
    }
}
