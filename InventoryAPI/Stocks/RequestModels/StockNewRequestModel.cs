using InventoryAPI.Products.RequestModels;
using InventoryLogic.Stocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Stocks.RequestModels
{
    public class StockNewRequestModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        [JsonConstructor]
        private StockNewRequestModel(int ProductId, int Amount, DateTime Date)
        {
            this.ProductId = ProductId;
            this.Amount = Amount;
            this.Date = Date;
        }

        // system converter
        public static StockDTO StockNewRequestModelToStockDTO(StockNewRequestModel stock)
        {
            return new StockDTO()
            {
                Id = 0,
                Amount = stock.Amount,
                ProductId = stock.ProductId, 
                //Product = ProductRequestModel.ProductDTOToProductRequestModel(stock.Product), error in logic layer TODO, stockDTO must contain ProductDTO, not Product
                Date = stock.Date
            };
        }

    }
}
