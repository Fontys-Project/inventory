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
        public List<ProductRequestChildModel> Products { get; set; }

        private TagRequestModel(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            this.Products = new List<ProductRequestChildModel>();
        }


        // system converter
        public static TagRequestModel TagDTOToTagRequestModel(TagDTO tag)
        {
            var tagObj = new TagRequestModel(tag.Id, tag.Name);

            tag.Products.ForEach(product => tagObj.Products.Add(ProductRequestChildModel.ProductDTOToProductRequestChildModel(product)));

            return tagObj;
        }

        // system converter
        public static TagDTO TagRequestModelToTagDTO(TagRequestModel tag)
        {
            return new TagDTO()
            {
                Id = tag.Id,
                Name = tag.Name,
                Products = null // TODO
            };
        }
    }
}
