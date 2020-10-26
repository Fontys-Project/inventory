using InventoryLogic.Stocks;
using InventoryDAL.Database;

namespace InventoryDAL.Stocks
{
    public class StockMySqlDAO : MySqlDAO<Stock>
    {
        public StockMySqlDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}