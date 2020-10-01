using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;
using InventoryDAL.Database;
using InventoryLogic.Facade;

namespace InventoryDI.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private IProductDAO productDAO;

        public DatabaseFactory()
        {
            productDAO = new ProductMockDAO();
        }

        public IProductDAO GetProductDAO()
        {
            return productDAO;
        }
    }
}
