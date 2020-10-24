using InventoryLogic.Stocks;
using InventoryDAL.Database;

namespace InventoryDAL.Stocks
{
    public class StockMySqlDAO : MySqlDAO<Stock>, IStockDAO
    {
        public StockMySqlDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}