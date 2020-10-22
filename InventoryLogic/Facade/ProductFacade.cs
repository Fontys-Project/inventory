using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;

namespace InventoryLogic.Facade
{
    public class ProductFacade : IFacade<Product.Product>
    {
        private readonly IDatabaseFactory databaseFactory;

        public ProductFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Product.Product Get(int id)
        {
            return databaseFactory.GetProductDAO().GetProduct(id);
        }


        public List<Product.Product> GetAll()
        {
            return databaseFactory.GetProductDAO().GetAllProducts();
        } 
        
        public Product.Product Add(Product.Product product)
        {
            databaseFactory.GetProductDAO().AddProduct(product);
            return product;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetProductDAO().RemoveProduct(id);
            return true;
        }

        public Boolean Modify(Product.Product product, int id)
        {
            databaseFactory.GetProductDAO().ModifyProduct(product, id);
            return true;
        }
    }
}
