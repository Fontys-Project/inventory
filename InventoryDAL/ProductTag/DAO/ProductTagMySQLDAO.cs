using InventoryDAL.Database;
using InventoryDAL.Interfaces;
using System.Linq;

namespace InventoryDAL.ProductTag
{
    public class ProductTagMySQLDAO : MySqlDAO<ProductTagEntity>, IProductTagDAO
    {
        public ProductTagMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        //besides crud methods:

        public ProductTagEntity Get(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault();
        }

        public void Remove(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Remove(
                this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault()
                );
            this.dbContext.SaveChangesAsync();
        }
    }
}
