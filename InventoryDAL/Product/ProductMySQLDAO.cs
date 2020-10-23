using InventoryLogic.Products;
using InventoryDAL.Database;

namespace InventoryDAL.Products
{
    public class ProductMySqlDAO : MySqlDAO<Product>, IProductDAO
    {
        public ProductMySqlDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}
