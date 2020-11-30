using InventoryDAL.Database;
using InventoryDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDAL.ProductTag
{
    public class ProductTagMySQLDAO : MySqlDAO<ProductTagEntity>, IProductTagDAO
    {
        public ProductTagMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        public override List<ProductTagEntity> GetAllWithNavigationProperties()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<ProductTagEntity>> lst = this.Table
                .Include(pte => pte.ProductEntity)
                .Include(pte => pte.TagEntity)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }

        //besides generic crud methods:

        public ProductTagEntity Get(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault();
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
