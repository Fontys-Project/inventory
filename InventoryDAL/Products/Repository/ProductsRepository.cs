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

        public void Add(Product product)
        {
            ProductEntity productEntity = BuildProductEntity(product);
            productEntityDAO.Add(productEntity);
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
            return productBuilder.Build();
        }

        private ProductEntity BuildProductEntity(Product product)
        {
            var productEntityBuilder = builderFactory.CreateProductEntityBuilder(product);
            return productEntityBuilder.Build();
        }
    }
}
