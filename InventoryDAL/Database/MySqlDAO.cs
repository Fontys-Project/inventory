using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventoryDAL.Database
{
    public abstract class MySqlDAO<T> where T : class
    {
        private readonly MySqlContext dbContext;
        private DbSet<T> Table { get; set; }

        public MySqlDAO(MySqlContext context)
        {
            this.dbContext = context;
            this.Table = context.Set<T>();
        }

        public void Add(T obj)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Add(obj);
            this.dbContext.SaveChangesAsync();
        }

        public List<T> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<T>> lst = this.Table.ToListAsync();
            lst.Wait(); // TODO: beter async uitwerken?
            return lst.Result;
        }

        public T Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Find(id);
        }

        public void Modify(T obj)
        {
            this.dbContext.Database.EnsureCreated();
            this.dbContext.Update(obj);
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

