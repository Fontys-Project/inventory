using InventoryLogic.Products;
using InventoryDAL.Database;
using InventoryLogic.Facade;
using InventoryLogic.ProductTagJoins;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace InventoryDAL.ProductTagJoins
{
    public class ProductTagJoinMySQLDAO : ICrudDAO<ProductTagJoin>
    {
        private readonly MySqlContext dbContext;
        private DbSet<ProductTagJoin> Table { get; set; }

        public ProductTagJoinMySQLDAO(MySqlContext context)
        {
            this.dbContext = context;
            this.Table = context.Set<ProductTagJoin>();
        }

        public void Add(ProductTagJoin obj)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Add(obj);
            this.dbContext.SaveChangesAsync();
        }

        public List<ProductTagJoin> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<ProductTagJoin>> lst = this.Table.ToListAsync();
            lst.Wait(); // TODO: beter async uitwerken?
            return lst.Result;
        }

        public ProductTagJoin Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public ProductTagJoin Get(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault();
        }

        public void Modify(ProductTagJoin obj)
        {
            this.dbContext.Database.EnsureCreated();
            this.dbContext.Update(obj);
            this.dbContext.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Remove(
                this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault()
                );
            this.dbContext.SaveChangesAsync();
        }
    }
}
