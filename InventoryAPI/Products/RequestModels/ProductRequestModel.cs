using InventoryAPI.Stocks.RequestModels;
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
        public List<StockRequestModel> StockRequestModels { get; private set; }

        public ProductRequestModel()
        {
            StockRequestModels = new List<StockRequestModel>();
        }

        // system converter
        public static ProductRequestModel ProductDTOToProductRequestModel(ProductDTO productDTO)
        {
            ProductRequestModel productRequestModel = new ProductRequestModel()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Sku = productDTO.Sku
            };

            productDTO.Stocks.ForEach(stock =>
            {
                productRequestModel.StockRequestModels.Add(StockRequestModel.StockDTOToStockRequestModel(stock));
            });

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
