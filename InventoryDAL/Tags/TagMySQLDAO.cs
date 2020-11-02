using InventoryLogic.Tags;
using InventoryDAL.Database;
using InventoryDAL.Tags;
using InventoryLogic.Facade;

namespace InventoryDAL.ProductTags
{
    public class TagMySQLDAO : MySqlDAO<Tag,TagEntity>
    {
        public TagMySQLDAO(MySqlContext context, IDatabaseFactory databaseFactory)
            : base(context, databaseFactory)
        {

        }
    }
}
