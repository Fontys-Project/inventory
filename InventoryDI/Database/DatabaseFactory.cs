using InventoryLogic.Facade;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;
using InventoryDAL.Database;
using InventoryDAL.Products;
using InventoryDAL.ProductTags;
using InventoryDAL.Stocks;

namespace InventoryDI.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IProductDAO productDAO;
        private readonly ITagDAO tagDAO;
        private readonly IStockDAO stockDAO;

        public DatabaseFactory(DatabaseType databaseType)
        {
            switch(databaseType)
            {
                case DatabaseType.MOCK: 
                    productDAO = new ProductMockDAO();
                    tagDAO = new TagMockDAO();
                    stockDAO = new StockMockDAO();
                    break;
                case DatabaseType.MYSQL:
                    var context = new MySqlContext();
                    productDAO = new ProductMySqlDAO(context);
                    tagDAO = new TagMySQLDAO(context);
                    stockDAO = new StockMySqlDAO(context);
                    break;
            }
        }

        public IProductDAO GetProductDAO()
        {
            return productDAO;
        }

        public ITagDAO GetTagDAO()
        {
            return tagDAO;
        }

        public IStockDAO GetStockDAO()
        {
            return stockDAO;
        }
    }
}
