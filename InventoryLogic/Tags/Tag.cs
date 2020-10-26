using InventoryLogic.ProductTagJoins;
using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductTagJoin> ProductTagJoins { get; set; }

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
