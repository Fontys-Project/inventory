using InventoryDAL.Database;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Interfaces;
using InventoryDAL.Tags;

namespace InventoryDI
{
    public class DAOFactory : IDAOFactory
    {
        public IProductEntityDAO ProductEntityDAO { get; }
        public IStockEntityDAO StockEntityDAO { get; }
        public ITagEntityDAO TagEntityDAO { get; }
        public IProductTagDAO ProductTagDAO { get; }

        public DAOFactory(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.MOCK:
                    ProductEntityDAO = new ProductEntityMockDAO();
                    TagEntityDAO = new TagEntityMockDAO();
                    StockEntityDAO = new StockEntityMockDAO();
                    break;
                case DatabaseType.MYSQL:
                    var context = new MySqlContext();
                    ProductEntityDAO = new ProductEntityMySQLDAO(context);
                    StockEntityDAO = new StockEntityMySQLDAO(context);
                    TagEntityDAO = new TagEntityMySQLDAO(context);
                    ProductTagDAO = new ProductTagMySQLDAO(context);
                    break;
            }
        }

        public ICrudDAO<T> GetCrudDAO<T>()
        {
            // Just for crud actions. If you need more actions from your DAO 
            // you will need to call property directly

            if (typeof(T) == typeof(ProductEntity))
                return (ICrudDAO<T>)ProductEntityDAO;
            if (typeof(T) == typeof(StockEntity))
                return (ICrudDAO<T>)StockEntityDAO;
            if (typeof(T) == typeof(TagEntity))
                return (ICrudDAO<T>)TagEntityDAO;
            return null;
        }
    }
}
