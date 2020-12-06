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

        public List<Product> GetAllExcludingNavigationProperties()
        {
            List<ProductEntity> productEntities = productEntityDAO.GetAllIncludingNavigationProperties();
            return productEntities
                .Select(productEntity => BuildProduct(productEntity, false))
                .ToList();
        }

        public List<Product> GetAll()
        {
            List<ProductEntity> productEntities = productEntityDAO.GetAllIncludingNavigationProperties();
            return productEntities
                .Select(productEntity => BuildProduct(productEntity, true))
                .ToList();
        }

        //public List<Product> GetAll(int tagId)
        //{
        //    List<ProductEntity> productEntities = productEntityDAO.GetAllIncludingNavigationProperties().Where();
        //    return productEntities
        //        .Select(productEntity => BuildProduct(productEntity, true))
        //        .ToList();
        //}

        public Product GetExcludingNavigationProperties(int id)
        {
            ProductEntity productEntity = productEntityDAO.GetIncludingNavigationProperties(id);
            return BuildProduct(productEntity, false);
        }

        public Product Get(int id)
        {
            ProductEntity productEntity = productEntityDAO.GetIncludingNavigationProperties(id);
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
