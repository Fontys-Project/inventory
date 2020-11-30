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

        public override List<StockEntity> GetAllWithNavigationProperties()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<StockEntity>> lst = this.Table
                .Include(se => se.ProductEntity)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }
    }
}