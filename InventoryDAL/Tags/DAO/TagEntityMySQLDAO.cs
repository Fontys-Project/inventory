using InventoryLogic.Tags;
using InventoryDAL.Database;
using InventoryDAL.Tags;
using InventoryLogic.Facade;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryDAL.ProductTag
{
    public class TagEntityMySQLDAO : MySqlDAO<TagEntity>, ITagEntityDAO
    {
        public TagEntityMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        public override List<TagEntity> GetAllWithNavigationProperties()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<TagEntity>> lst = this.Table
                .Include(te => te.ProductTagEntities)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }
    }
}
