using InventoryLogic.Products;
using InventoryDAL.Database;
using InventoryLogic.Facade;
using Microsoft.EntityFrameworkCore;
using InventoryDAL.ProductTag;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryDAL.Products
{
    public class ProductEntityMySQLDAO : MySqlDAO<ProductEntity>, IProductEntityDAO
    {
        public DbSet<ProductTagEntity> ProductTagsTable { get; private set; }

        public ProductEntityMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        public override List<ProductEntity> GetAllIncludingNavigationProperties()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<ProductEntity>> lst = this.Table
                .Include(pe => pe.ProductTagEntities)
                .Include(pe => pe.StockEntities)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }

        public override ProductEntity GetIncludingNavigationProperties(int id)
        {
            this.dbContext.Database.EnsureCreated();
            // these includes force check the db; it ignores local cache...
            Task<ProductEntity> productEntity = this.Table
                .Include(pe => pe.ProductTagEntities)
                .Include(pe => pe.StockEntities)
                .SingleOrDefaultAsync(pe => pe.Id == id);
            productEntity.Wait();
            return productEntity.Result;
        }
    }
}
