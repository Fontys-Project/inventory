using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryDAL.Database;
using InventoryDAL.Stocks.StockEntities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryDAL.Stocks.StockEntities
{
    public class StockEntityMySQLDAO : MySqlDAO<StockEntity>, IStockEntityDAO
    {
        public StockEntityMySQLDAO(MySqlContext context)
            : base(context)
        {

        }

        public override List<StockEntity> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<StockEntity>> lst = this.Table
                .Include(se => se.ProductEntity)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }

        public override StockEntity Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            // these includes force checking the db; it ignores local cache...
            Task<StockEntity> stockEntity = this.Table
                .Include(se => se.ProductEntity)
                .SingleOrDefaultAsync(se => se.Id == id);
            stockEntity.Wait();
            return stockEntity.Result;
        }
    }
}