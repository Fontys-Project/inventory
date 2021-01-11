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
        public List<TagRequestChildModel> Tags { get; private set; }
             
        private ProductRequestModel(int Id,string Name, decimal Price, string Sku)
        {
            Stocks = new List<StockRequestChildModel>();
            Tags = new List<TagRequestChildModel>();
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Sku = Sku;
        }

        // system converter
        public static ProductRequestModel ProductDTOToProductRequestModel(ProductDTO productDTO)
        {
            var productRequestModel = new ProductRequestModel(productDTO.Id, productDTO.Name, productDTO.Price, productDTO.Sku);

            productDTO.Stocks.ForEach(stock => productRequestModel.Stocks.Add(StockRequestChildModel.StockDTOToStockRequestChildModel(stock)));

            productDTO.Tags.ForEach(tag => productRequestModel.Tags.Add(TagRequestChildModel.TagDTOToTagRequestChildModel(tag)));

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
