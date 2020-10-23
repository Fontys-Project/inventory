using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;
using InventoryDAL.Products;
using InventoryLogic.Facade;
using InventoryDAL.ProductTags;
using InventoryLogic.ProductTags;
using InventoryDAL.Database;

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
                    var context = new MySqlContext();
                    productDAO = new ProductMySqlDAO(context);
                    productTagDAO = new ProductTagMySqlDAO(context);
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
