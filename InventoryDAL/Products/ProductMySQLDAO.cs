using InventoryLogic.Products;
using InventoryDAL.Database;
using InventoryLogic.Facade;
using Microsoft.EntityFrameworkCore;
using InventoryDAL.ProductTagJoins;

namespace InventoryDAL.Products
{
    public class ProductMySQLDAO : MySqlDAO<Product,ProductEntity>
    {
        public DbSet<ProductTagJoinEntity> ProductTagsTable { get; private set; }

        public ProductMySQLDAO(MySqlContext context, IDatabaseFactory databaseFactory)
            : base(context,databaseFactory)
        {
            this.ProductTagsTable = context.Set<ProductTagJoinEntity>();
        }
    }
}
