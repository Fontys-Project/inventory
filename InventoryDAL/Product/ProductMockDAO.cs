using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;

namespace InventoryDAL.Product
{

    public class ProductMockDAO : IProductDAO
    {

        private List<InventoryLogic.Product.Product> products;

        public ProductMockDAO()
        {
            products = new List<InventoryLogic.Product.Product>();
            products.Add(new InventoryLogic.Product.Product(1, "Mondkapje", 100, Environment.GetEnvironmentVariable("TEST")));
        }


        public void AddProduct(InventoryLogic.Product.Product product)
        {
            products.Add(product);
        }

        public List<InventoryLogic.Product.Product> GetAllProducts()
        {
            return products;
        }

        public InventoryLogic.Product.Product GetProduct(int ID)
        {
            foreach(InventoryLogic.Product.Product product in products)
            {
                if (product.Id == ID)
                    return product;
            }

            return null;
        }

        public void ModifyProduct(InventoryLogic.Product.Product product, int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
