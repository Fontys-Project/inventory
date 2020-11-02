using InventoryLogic.Facade;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;
using InventoryDAL.Database;
using InventoryDAL.Products;
using InventoryDAL.ProductTags;
using InventoryDAL.Stocks;
using InventoryDAL.ProductTagJoins;
using InventoryLogic.Crud;
using InventoryLogic.ProductTagJoins;

namespace InventoryDI.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public ICrudDAO<Product> ProductDAO { get; }
        public ICrudDAO<Stock> StockDAO { get; }
        public ICrudDAO<Tag> TagDAO { get; }

        //public ICrudDAO<ProductTagJoin> ProductTagJoinDAO { get; }

        public DatabaseFactory(DatabaseType databaseType)
        {
            switch(databaseType)
            {
                case DatabaseType.MOCK: 
                    ProductDAO = new ProductMockDAO();
                    //TagDAO = new TagMockDAO();
                    StockDAO = new StockMockDAO();
                    break;
                case DatabaseType.MYSQL:
                    var context = new MySqlContext();
                    ProductDAO = new ProductMySQLDAO(context,this);
                    StockDAO = new StockMySqlDAO(context,this);
                    TagDAO = new TagMySQLDAO(context,this);
                    //ProductTagJoinDAO = new ProductTagJoinMySQLDAO(context,this);
                    break;
            }
        }

        public ICrudDAO<T> GetCrudDAO<T>()
        {
            // Just for crud actions. If you add more actions to your DAO 
            // you will need to change property type and call it directly

            if (typeof(T) == typeof(Product))
                return (ICrudDAO<T>)ProductDAO;
            if (typeof(T) == typeof(Stock))
                return (ICrudDAO<T>)StockDAO;
            if (typeof(T) == typeof(Tag))
                return (ICrudDAO<T>)TagDAO;
            return null;
        }
    }
}
