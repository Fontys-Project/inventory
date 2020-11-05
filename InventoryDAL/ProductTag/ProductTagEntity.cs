using InventoryDAL.Products;
using InventoryDAL.Tags;

namespace InventoryDAL.ProductTag
{
    public class ProductTagEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductEntity ProductEntity { get; set; }

        public int TagId { get; set; }
        public TagEntity TagEntity { get; set; }
    }
}
