using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryDAL.Interfaces;
using InventoryDAL.Database;

namespace InventoryDAL.Products
{
    public class ProductEntityMySQLDAO : ICrudDAO<ProductEntity>, IProductEntityDAO
    {
        public readonly MySqlContext dbContext;
        public DbSet<ProductEntity> ProductEntityTable { get; private set; }

        public ProductEntityMySQLDAO(MySqlContext context)
        {
            this.dbContext = context;
            this.ProductEntityTable = context.Set<ProductEntity>();
        }

        public ProductEntity Add(ProductEntity e)
        {
            this.dbContext.Database.EnsureCreated();
            this.ProductEntityTable.Add(e);
            this.dbContext.SaveChangesAsync();
            return e;
        }

        public List<ProductEntity> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<ProductEntity>> lst = this.ProductEntityTable
                //.Include(pe => pe.ProductTagEntities)
                .ToListAsync(); 
            lst.Wait();
            return lst.Result;
        }

        public ProductEntity Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            return this.ProductEntityTable.Find(id);
        }

        public void Modify(ProductEntity e)
        {
            this.dbContext.Database.EnsureCreated();
            this.dbContext.Update(e);
            this.dbContext.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            this.dbContext.Database.EnsureCreated();
            this.ProductEntityTable.Remove(this.ProductEntityTable.Find(id));
            this.dbContext.SaveChangesAsync();
        }
    }
}

