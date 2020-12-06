using InventoryAPI.Products.RequestModels;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Tags.RequestModels
{
    public class TagRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductRequestModel> Products { get; set; }


        // system converter
        public static TagRequestModel TagDTOToTagRequestModel(TagDTO tag)
        {
            return new TagRequestModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                Products = null // TODO
            };
        }

        // system converter
        public static TagDTO TagRequestModelToTagDTO(TagRequestModel tag)
        {
            return new TagDTO()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }
    }
}
