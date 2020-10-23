using InventoryLogic.ProductTags;
using InventoryDAL.Database;

namespace InventoryDAL.ProductTags
{
    public class ProductTagMySqlDAO : MySqlDAO<ProductTag>, IProductTagDAO
    {
        public ProductTagMySqlDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}
