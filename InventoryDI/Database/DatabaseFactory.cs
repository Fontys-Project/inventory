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
        public ICrudDAO<Product> ProductDAO { get; }
        public ICrudDAO<Tag> TagDAO { get; }
        public ICrudDAO<Stock> StockDAO { get; }

        public DatabaseFactory(DatabaseType databaseType)
        {
            switch(databaseType)
            {
                case DatabaseType.MOCK: 
                    ProductDAO = new ProductMockDAO();
                    TagDAO = new TagMockDAO();
                    StockDAO = new StockMockDAO();
                    break;
                case DatabaseType.MYSQL:
                    var context = new MySqlContext();
                    ProductDAO = new ProductMySqlDAO(context);
                    TagDAO = new TagMySQLDAO(context);
                    StockDAO = new StockMySqlDAO(context);
                    break;
            }
        }

        public ICrudDAO<T> GetCrudDAO<T>()
        {
            if (typeof(T) == typeof(Product))
                return (ICrudDAO<T>)ProductDAO;
            if (typeof(T) == typeof(Tag))
                return (ICrudDAO<T>)TagDAO;
            if (typeof(T) == typeof(Stock))
                return (ICrudDAO<T>)StockDAO;
            return null;
        }
    }
}
