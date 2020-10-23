using InventoryLogic.Products;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryDAL.Database;

namespace InventoryDAL.Database
{
    public abstract class MySqlDAO<Type> where Type : class
    {
        private readonly MySqlContext dbContext;
        private DbSet<Type> Table { get; set; }

        public MySqlDAO(MySqlContext context)
        {
            this.dbContext = context;
            this.Table = context.Set<Type>();
        }

        public void Add(Type obj)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Add(obj);
            this.dbContext.SaveChangesAsync();
        }

        public List<Type> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<Type>> lst = this.Table.ToListAsync();
            lst.Wait(); // TODO: beter async uitwerken?
            return lst.Result;
        }

        public Type Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Find(id);
        }

        public void Modify(Type obj, int id)
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

