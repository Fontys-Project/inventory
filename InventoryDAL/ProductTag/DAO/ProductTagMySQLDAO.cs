using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryDAL.Database;
using Microsoft.EntityFrameworkCore;

namespace InventoryDAL.ProductTag
{
    public class ProductTagMySQLDAO : MySqlDAO<ProductTagEntity>, IProductTagDAO
    {
        public ProductTagMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        public override List<ProductTagEntity> GetAllIncludingNavigationProperties()
        {
            dbContext.Database.EnsureCreated();
            Task<List<ProductTagEntity>> lst = Table
                .Include(pte => pte.ProductEntity)
                .Include(pte => pte.TagEntity)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }

        //besides generic crud methods:

        public ProductTagEntity GetExcludingNavigationProperties(int productId, int tagId)
        {
            dbContext.Database.EnsureCreated();
            Task<ProductTagEntity> productTagEntity = this.Table
                .SingleOrDefaultAsync(pte => pte.ProductId == productId && pte.TagId == tagId);
            productTagEntity.Wait();
            return productTagEntity.Result;
        }

        public ProductTagEntity GetIncludingNavigationProperties(int productId, int tagId)
        {
            dbContext.Database.EnsureCreated();
            // these includes force checking the db; it ignores local cache...
            Task<ProductTagEntity> productTagEntity = this.Table
                .Include(pte => pte.ProductEntity)
                .Include(pte => pte.TagEntity)
                .SingleOrDefaultAsync(pte => pte.ProductId == productId && pte.TagId == tagId);
            productTagEntity.Wait();
            return productTagEntity.Result;
        }

        public void Remove(int productId, int tagId)
        {
            dbContext.Database.EnsureCreated();
            Table.Remove(
                Table.FirstOrDefault(j => j.ProductId == productId && j.TagId == tagId) 
                ?? throw new InvalidOperationException()
            );
            dbContext.SaveChangesAsync();
        }
    }
}
