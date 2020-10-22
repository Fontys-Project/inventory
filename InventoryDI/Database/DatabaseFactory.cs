using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;
using InventoryDAL.Product;
using InventoryLogic.Facade;
using InventoryDAL.ProductTags;
using InventoryLogic.ProductTags;

namespace InventoryDI.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IProductDAO productDAO;
        private readonly IProductTagDAO productTagDAO;

        public DatabaseFactory(DatabaseType databaseType)
        {
            switch(databaseType)
            {
                case DatabaseType.MOCK: 
                    productDAO = new ProductMockDAO();
                    productTagDAO = new ProductTagMockDAO();
                    break;
                case DatabaseType.MYSQL: 
                    productDAO = new ProductMySQLDAO();
                    productTagDAO = new ProductTagMySQLDAO();
                    break;
            }
        }

        public IProductDAO GetProductDAO()
        {
            return productDAO;
        }

        public IProductTagDAO GetProductTagDAO()
        {
            return productTagDAO;
        }
    }
}
