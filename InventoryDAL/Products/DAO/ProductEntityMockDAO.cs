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
                new ProductEntity(1, "Mondkapje", 100, Environment.GetEnvironmentVariable("TEST"))
            };
        }

        public void Add(ProductEntity product)
        {
            productsEntities.Add(product);
        }

        public List<ProductEntity> GetAll()
        {
            return productsEntities;
        }

        public ProductEntity Get(int ID)
        {
            foreach(ProductEntity product in productsEntities)
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
