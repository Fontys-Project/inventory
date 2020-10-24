using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;
using InventoryDAL.Products;
using InventoryLogic.Facade;
using InventoryDAL.ProductTags;
using InventoryLogic.ProductTags;
using InventoryDAL.Database;
using InventoryDAL.Stocks;
using InventoryLogic.Stocks;

namespace InventoryDI.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IProductDAO productDAO;
        private readonly IProductTagDAO productTagDAO;
        private readonly IStockDAO stockDAO;

        public DatabaseFactory(DatabaseType databaseType)
        {
            switch(databaseType)
            {
                case DatabaseType.MOCK: 
                    productDAO = new ProductMockDAO();
                    productTagDAO = new ProductTagMockDAO();
                    stockDAO = new StockMockDAO();
                    break;
                case DatabaseType.MYSQL:
                    var context = new MySqlContext();
                    productDAO = new ProductMySqlDAO(context);
                    productTagDAO = new ProductTagMySqlDAO(context);
                    stockDAO = new StockMySqlDAO(context);
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

        public IStockDAO GetStockDAO()
        {
            return stockDAO;
        }
    }
}
