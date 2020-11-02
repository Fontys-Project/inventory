using InventoryDAL.Products;
using InventoryDAL.Tags;

namespace InventoryDAL.ProductTagJoins
{
    public class ProductTagJoinEntity
    {
        public int ProductId { get; set; }
        public ProductEntity ProductEntity { get; set; }

        public int TagId { get; set; }
        public TagEntity TagEntity { get; set; }
    }
}
