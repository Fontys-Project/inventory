using InventoryAPI.Products.RequestModels;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Tags.RequestModels
{
    public class AddTagRequestModel
    {
        public string Name { get; set; }

        [JsonConstructor]
        private AddTagRequestModel(string Name)
        {
            this.Name = Name;
        }

        // system converter
        public static TagDTO AddTagRequestModelToTagDTO(AddTagRequestModel tag)
        {
            return new TagDTO()
            {
                Id = 0, 
                Name = tag.Name,
                Products = new List<ProductDTO>() // TODO
            };
        }
    }
}
