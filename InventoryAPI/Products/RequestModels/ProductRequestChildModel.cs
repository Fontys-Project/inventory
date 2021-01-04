using InventoryLogic.Products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Products.RequestModels
{
    public class ProductRequestChildModel
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int Id { get; set; }
       
        [JsonConstructor]
        private ProductRequestChildModel(int Id, string Name, decimal Price, string Sku)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Sku = Sku;
        }

        // system converter
        public static ProductRequestChildModel ProductDTOToProductRequestChildModel(ProductDTO productDTO)
        {
            var productRequestChildModel = new ProductRequestChildModel(productDTO.Id, productDTO.Name, productDTO.Price, productDTO.Sku);

            return productRequestChildModel;
        }

        public static ProductDTO ProductRequestChildModelToProductDTO(ProductRequestChildModel productRequestChildModel)
        {
            return new ProductDTO()
            {
                Id = productRequestChildModel.Id,
                Name = productRequestChildModel.Name,
                Price = productRequestChildModel.Price,
                Sku = productRequestChildModel.Sku
            };
        }


    }
}
