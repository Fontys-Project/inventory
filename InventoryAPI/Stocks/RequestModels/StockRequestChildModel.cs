using InventoryAPI.Products.RequestModels;
using InventoryLogic.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Stocks.RequestModels
{
    public class StockRequestChildModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // system converter
        public static StockRequestChildModel StockDTOToStockRequestChildModel(StockDTO stock)
        {
            return new StockRequestChildModel()
            {
                Id = stock.Id,
                Amount = stock.Amount,
                ProductId = stock.ProductId,
                //Product = ProductRequestModel.ProductDTOToProductRequestModel(stock.Product), error in logic layer TODO, stockDTO must contain ProductDTO, not Product
                Date = stock.Date
            };
        }

        // system converter
        public static StockDTO StockRequestChildModelToStockDTO(StockRequestChildModel stock)
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
