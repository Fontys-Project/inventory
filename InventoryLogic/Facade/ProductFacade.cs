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


        public List<Product.Product> GetProducts()
        {
            return databaseFactory.GetProductDAO().GetAllProducts();
        } 
        
        public void AddProduct(string name, int id, decimal price,string sku )
        {
            databaseFactory.GetProductDAO().AddProduct(new Product.Product(id,name,price,sku));
        }

    }
}
