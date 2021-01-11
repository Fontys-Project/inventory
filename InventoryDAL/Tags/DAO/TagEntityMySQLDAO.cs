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

        public new List<TagEntity> GetAll()
        {
            this.dbContext.Database.EnsureCreated();
            Task<List<TagEntity>> lst = this.Table
                .Include(te => te.ProductTagEntities)
                .ToListAsync();
            lst.Wait();
            return lst.Result;
        }

        public new TagEntity Get(int id)
        {
            this.dbContext.Database.EnsureCreated();
            // these includes force checking the db; it ignores local cache...
            Task<TagEntity> tagEntity = this.Table
                .Include(te => te.ProductTagEntities)
                .SingleOrDefaultAsync(te => te.Id == id);
            tagEntity.Wait();
            return tagEntity.Result;
        }
    }
}
