using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;

namespace InventoryDAL.Database
{

    public class ProductMockDAO : IProductDAO
    {

        private List<Product> products;

        public ProductMockDAO()
        {
            products = new List<Product>();
            products.Add(new Product(1, "Mondkapje", 100, "SKU123"));
        }


        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProduct(int ID)
        {
            foreach(Product product in products)
            {
                if (product.Id == ID)
                    return product;
            }

            return null;
        }

        public void ModifyProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
