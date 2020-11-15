using InventoryLogic.Products;
using InventoryDAL.Database;
using InventoryLogic.Facade;
using Microsoft.EntityFrameworkCore;
using InventoryDAL.ProductTag;

namespace InventoryDAL.Products
{
    public class ProductEntityMySQLDAO : MySqlDAO<ProductEntity>, IProductEntityDAO
    {
        public DbSet<ProductTagEntity> ProductTagsTable { get; private set; }

        public ProductEntityMySQLDAO(MySqlContext context)
            : base(context)
        {
        }
    }
}
