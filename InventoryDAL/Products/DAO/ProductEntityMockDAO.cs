using InventoryDAL.Interfaces;
using System;
using System.Collections.Generic;

namespace InventoryDAL.Products
{

    public class ProductEntityMockDAO : IProductEntityDAO
    {
        private readonly List<ProductEntity> productsEntities;

        public ProductEntityMockDAO()
        {
            productsEntities = new List<ProductEntity>
            {
                new ProductEntity{Id = 1, Name = "Mondkapje", Price = 100, Sku = Environment.GetEnvironmentVariable("TEST") }
            };
        }

        public ProductEntity Add(ProductEntity product)
        {
            productsEntities.Add(product);
            return product;
        }

        public List<ProductEntity> GetAllExcludingNavigationProperties()
        {
            return productsEntities;
        }

        public List<ProductEntity> GetAll()
        {
            return productsEntities;
        }

        public ProductEntity GetExcludingNavigationProperties(int ID)
        {
            foreach(ProductEntity product in productsEntities)
            {
                if (product.Id == ID)
                    return product;
            }

            return null;
        }

        public ProductEntity Get(int ID)
        {
            foreach (ProductEntity product in productsEntities)
            {
                if (product.Id == ID)
                    return product;
            }

            return null;
        }

        public void Modify(ProductEntity product)
        {
            throw new NotImplementedException();
        }

        public void Remove(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
