using InventoryLogic.Stocks;
using InventoryDAL.Database;
using InventoryLogic.Facade;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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