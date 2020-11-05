using InventoryLogic.Products;
using InventoryLogic.ProductTag;
using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public interface ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}