using InventoryAPI.Products.RequestModels;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Tags.RequestModels
{
    public class TagRequestChildModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private TagRequestChildModel(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }


        // system converter
        public static TagRequestChildModel TagDTOToTagRequestChildModel(TagDTO tag)
        {
            return new TagRequestChildModel(tag.Id, tag.Name);
        }

      
    }
}
