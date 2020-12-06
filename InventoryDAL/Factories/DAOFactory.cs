using InventoryDAL.Database;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.Products.ProductEntities.Mock;
using InventoryDAL.ProductTag;
using InventoryDAL.ProductTag.Interfaces;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.StockEntities.Interfaces;
using InventoryDAL.Stocks.StockEntities.Mock;
using InventoryDAL.Tags;

namespace InventoryDAL.Factories
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

        public IHasCrudActions<T> GetCrudDAO<T>()
        {
            // Just for crud actions. If you need more actions from your DAO 
            // you will need to call property directly

            if (typeof(T) == typeof(ProductEntity))
                return (IHasCrudActions<T>)ProductEntityDAO;
            if (typeof(T) == typeof(StockEntity))
                return (IHasCrudActions<T>)StockEntityDAO;
            if (typeof(T) == typeof(TagEntity))
                return (IHasCrudActions<T>)TagEntityDAO;
            return null;
        }
    }
}
