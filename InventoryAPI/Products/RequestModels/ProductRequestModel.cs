using InventoryAPI.Stocks.RequestModels;
using InventoryAPI.Tags.RequestModels;
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
        public List<StockRequestChildModel> Stocks { get; private set; }
        public List<TagRequestModel> Tags { get; private set; }
             
        public ProductRequestModel()
        {
            Stocks = new List<StockRequestChildModel>();
            Tags = new List<TagRequestModel>();
        }

        // system converter
        public static ProductRequestModel ProductDTOToProductRequestModel(ProductDTO productDTO)
        {
            var productRequestModel = new ProductRequestModel()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Sku = productDTO.Sku
            };

            productDTO.Stocks.ForEach(stock => productRequestModel.Stocks.Add(StockRequestModel.StockDTOToStockRequestModel(stock)));

            productDTO.Tags.ForEach(tag => productRequestModel.Tags.Add(TagRequestModel.TagDTOToTagRequestModel(tag)));

            return productRequestModel;
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
