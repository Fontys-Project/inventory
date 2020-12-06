using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryDAL.Database;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.ProductTag;
using Microsoft.EntityFrameworkCore;

namespace InventoryDAL.Products.ProductEntities
{
    public class ProductEntityMySQLDAO : MySqlDAO<ProductEntity>, IProductEntityDAO
    {
        public DbSet<ProductTagEntity> ProductTagsTable { get; private set; }

        public ProductEntityMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        public override List<ProductEntity> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<ProductEntity>> lst = this.Table
                .Include(pe => pe.ProductTagEntities)
                .Include(pe => pe.StockEntities)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }

        public override ProductEntity Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            // these includes force checking the db; it ignores local cache...
            Task<ProductEntity> productEntity = this.Table
                .Include(pe => pe.ProductTagEntities)
                .Include(pe => pe.StockEntities)
                .SingleOrDefaultAsync(pe => pe.Id == id);
            productEntity.Wait();
            return productEntity.Result;
        }
    }
}
