using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;

namespace InventoryLogic.Facade
{
    public class ProductFacade
    {

        private readonly IDatabaseFactory databaseFactory;

        public ProductFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Product.Product GetProduct(int id)
        {
            return databaseFactory.GetProductDAO().GetProduct(id);
        }


        public List<Product.Product> GetProducts()
        {
            return databaseFactory.GetProductDAO().GetAllProducts();
        } 
        
        public Product.Product AddProduct(string name, int id, decimal price,string sku )
        {
            Product.Product product = new Product.Product(id, name, price, sku);
            databaseFactory.GetProductDAO().AddProduct(product);
            return product;
        }

        public Boolean RemoveProduct(int id)
        {
            databaseFactory.GetProductDAO().RemoveProduct(id);
            return true;
        }

        public Boolean ModifyProduct(Product.Product product)
        {
            databaseFactory.GetProductDAO().ModifyProduct(product);
            return true;

        }

    }
}
