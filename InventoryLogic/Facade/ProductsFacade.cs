using InventoryLogic.Products;
using System;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : IFacade<Product>
    {
        private readonly IDatabaseFactory databaseFactory;

        public ProductsFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Product Get(int id)
        {
            return databaseFactory.GetProductDAO().Get(id);
        }


        public List<Product> GetAll()
        {
            return databaseFactory.GetProductDAO().GetAll();
        } 
        
        public Product Add(Product product)
        {
            databaseFactory.GetProductDAO().Add(product);
            return product;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetProductDAO().Remove(id);
            return true;
        }

        public Boolean Modify(Product product)
        {
            databaseFactory.GetProductDAO().Modify(product);
            return true;
        }
    }
}
