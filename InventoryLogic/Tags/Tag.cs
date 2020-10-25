using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public class Tag
    {
        public int Id { get; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        // Constructor used by .net API framwork
        public Tag()
        {

        }

        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
