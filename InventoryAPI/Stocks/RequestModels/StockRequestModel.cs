using InventoryAPI.Products.RequestModels;
using InventoryLogic.Stocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Stocks.RequestModels
{
    public class StockRequestModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public ProductRequestChildModel Product { get; set; }

        [JsonConstructor]
        private StockRequestModel(int Id, int ProductId, int Amount, DateTime Date) : base()
        {
            this.Id = Id;
            this.ProductId = ProductId;
            this.Amount = Amount;
            this.Date = Date;

        }


        public static StockRequestModel StockDTOToStockRequestModel(StockDTO stock)
        {
            return new StockRequestModel(stock.Id, stock.ProductId, stock.Amount, stock.Date)
            {
                
                Product = stock.Product != null ? ProductRequestChildModel.ProductDTOToProductRequestChildModel(stock.Product) : null,
               
            };
        }

        // system converter
        public static StockDTO StockRequestModelToStockDTO(StockRequestModel stock)
        {
            return new StockDTO()
            {
                Id = stock.Id,
                Amount = stock.Amount,
                ProductId = stock.ProductId,
                //Product = ProductRequestModel.ProductDTOToProductRequestModel(stock.Product), error in logic layer TODO, stockDTO must contain ProductDTO, not Product
                Date = stock.Date
            };
        }

    }
}
