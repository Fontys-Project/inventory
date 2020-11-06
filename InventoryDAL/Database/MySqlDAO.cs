using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryDAL.Interfaces;

namespace InventoryDAL.Database
{
    public abstract class MySqlDAO<EntityType> : ICrudDAO<EntityType> where EntityType : class
    {
        public readonly MySqlContext dbContext;
        public DbSet<EntityType> Table { get; private set; }

        public MySqlDAO(MySqlContext context)
        {
            this.dbContext = context;
            this.Table = context.Set<EntityType>();
        }

        public void Add(EntityType e)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Add(e);
            this.dbContext.SaveChangesAsync();
        }

        public List<EntityType> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<EntityType>> lst = this.Table.ToListAsync(); // TODO: No Tags in Product?? Swagger sometimes gives a failed to fetch.
            lst.Wait(); 
            return lst.Result;
        }

        public EntityType Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Find(id);
        }

        public void Modify(EntityType e)
        {
            this.dbContext.Database.EnsureCreated();
            this.dbContext.Update(e);
            this.dbContext.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Remove(this.Table.Find(id));
            this.dbContext.SaveChangesAsync();
        }
    }
}

