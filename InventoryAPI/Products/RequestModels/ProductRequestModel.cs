using InventoryLogic.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Products.RequestModels
{
    public class ProductRequestModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int Id { get; set; }


        // system converter
        public static ProductRequestModel ProductDTOToProductRequestModel(ProductDTO productDTO)
        {
            return new ProductRequestModel()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Sku = productDTO.Sku
            };
        }

        // system converter
        public static ProductDTO ProductRequestModelToProductDTO(ProductRequestModel productRequestModel)
        {
            return new ProductDTO()
            {
                Id = productRequestModel.Id,
                Name = productRequestModel.Name,
                Price = productRequestModel.Price,
                Sku = productRequestModel.Sku
            };
        }

    }
}
