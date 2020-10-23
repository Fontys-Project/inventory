using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;

namespace InventoryLogic.Facade
{
    public class ProductFacade : IFacade<Products.Product>
    {
        private readonly IDatabaseFactory databaseFactory;

        public ProductFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Products.Product Get(int id)
        {
            return databaseFactory.GetProductDAO().Get(id);
        }


        public List<Products.Product> GetAll()
        {
            return databaseFactory.GetProductDAO().GetAll();
        } 
        
        public Products.Product Add(Products.Product product)
        {
            databaseFactory.GetProductDAO().Add(product);
            return product;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetProductDAO().Remove(id);
            return true;
        }

        public Boolean Modify(Products.Product product)
        {
            databaseFactory.GetProductDAO().Modify(product);
            return true;
        }
    }
}
