using InventoryLogic.Tags;
using InventoryDAL.Database;
using InventoryDAL.Tags;
using InventoryLogic.Facade;

namespace InventoryDAL.ProductTag
{
    public class TagEntityMySQLDAO : MySqlDAO<TagEntity>, ITagEntityDAO
    {
        public TagEntityMySQLDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}
