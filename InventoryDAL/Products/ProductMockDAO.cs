using System;
using System.Collections.Generic;
using InventoryLogic.Products;

namespace InventoryDAL.Products
{

    public class ProductMockDAO : IProductDAO
    {
        private readonly List<Product> products;

        public ProductMockDAO()
        {
            products = new List<Product>
            {
                new Product(1, "Mondkapje", 100, Environment.GetEnvironmentVariable("TEST"))
            };
        }

        public void Add(Product product)
        {
            products.Add(product);
        }

        public List<Product> GetAll()
        {
            return products;
        }

        public Product Get(int ID)
        {
            foreach(Product product in products)
            {
                if (product.Id == ID)
                    return product;
            }

            return null;
        }

        public void Modify(Product product)
        {
            throw new NotImplementedException();
        }

        public void Remove(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
