using InventoryLogic.Stocks;
using InventoryDAL.Database;
using InventoryLogic.Facade;

namespace InventoryDAL.Stocks
{
    public class StockEntityMySQLDAO : MySqlDAO<StockEntity>, IStockEntityDAO
    {
        public StockEntityMySQLDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}