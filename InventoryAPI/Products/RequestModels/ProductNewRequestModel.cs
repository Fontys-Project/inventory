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

        private ProductNewRequestModel(string Name, decimal Price, string Sku)
        {
            this.Name = Name;
            this.Price = Price;
            this.Sku = Sku;
        }

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
