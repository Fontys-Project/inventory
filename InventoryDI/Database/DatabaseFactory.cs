using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;
using InventoryDAL.Product;
using InventoryLogic.Facade;

namespace InventoryDI.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private IProductDAO productDAO;

        public DatabaseFactory(DatabaseType databaseType)
        {
            switch(databaseType)
            {
                case DatabaseType.MOCK: productDAO = new ProductMockDAO(); break;
                case DatabaseType.MYSQL: productDAO = new ProductMySQLDAO(); break;
            }
            
        }

        public IProductDAO GetProductDAO()
        {
            return productDAO;
        }
    }
}
