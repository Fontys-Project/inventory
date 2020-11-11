using System.Collections.Generic;
using InventoryDAL.ProductTag;

namespace InventoryDAL.Tags
{
    public class TagEntity : ITagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagEntity()
        {
        }
    }
}
