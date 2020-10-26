using InventoryLogic.Products;
using InventoryDAL.Database;
using InventoryLogic.Facade;

namespace InventoryDAL.Products
{
    public class ProductMySQLDAO : MySqlDAO<Product>
    {
        public ProductMySQLDAO(MySqlContext context)
            : base(context)
        {

        }
    }
}
