using InventoryLogic.Tags;
using InventoryDAL.Database;

namespace InventoryDAL.ProductTags
{
    public class TagMySQLDAO : MySqlDAO<Tag>, ITagDAO
    {
        public TagMySQLDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}
