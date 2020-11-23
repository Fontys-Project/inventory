using InventoryLogic.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Products.RequestModels
{
    public class ProductNewRequestModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }

        // system converter
        public static ProductDTO ProductNewRequestModelToProductDTO(ProductNewRequestModel productRequestModel)
        {
            return new ProductDTO()
            {
                Name = productRequestModel.Name,
                Price = productRequestModel.Price,
                Sku = productRequestModel.Sku
            };
        }


    }
}
