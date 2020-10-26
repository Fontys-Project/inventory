using InventoryLogic.Products;
using InventoryLogic.Tags;

namespace InventoryLogic.ProductTagJoins
{
    public class ProductTagJoin
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
