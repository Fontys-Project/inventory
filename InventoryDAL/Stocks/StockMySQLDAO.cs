using InventoryLogic.Stocks;
using InventoryDAL.Database;
using InventoryLogic.Facade;

namespace InventoryDAL.Stocks
{
    public class StockMySqlDAO : MySqlDAO<Stock,StockEntity>
    {
        public StockMySqlDAO(MySqlContext context, IDatabaseFactory databaseFactory)
            : base(context, databaseFactory)
        {

        }
    }
}