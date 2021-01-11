using InventoryLogic.Stocks;
using InventoryLogic.Facade;
using InventoryAPI.Crud;
using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Stocks.RequestModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InventoryLogic.Products;

namespace InventoryAPI.Stocks
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class StocksController : ControllerBase
    {
        private readonly StocksFacade stocksFacade;
        private readonly ProductsFacade productsFacade;

        public StocksController(StocksFacade stocksFacade, ProductsFacade productsFacade)
        {
            this.stocksFacade = stocksFacade;
            this.productsFacade = productsFacade;
        }

        /// <summary>
        /// List of all Stock definitions
        /// </summary>
        [HttpGet]
        public List<StockRequestModel> GetAll()
        {
            var stocks = stocksFacade.GetAll();

            var stockRequestModels = stocks.ConvertAll(new System.Converter<StockDTO, StockRequestModel>(StockRequestModel.StockDTOToStockRequestModel));

            return stockRequestModels;
        }

        /// <summary>
        /// Get a specified Stock definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public StockRequestModel Get(int id)
        {
            return StockRequestModel.StockDTOToStockRequestModel(stocksFacade.Get(id));
        }

        /// <summary>
        /// Modify a Stock definition
        /// </summary>
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_stock_modify")]
        public bool Modify([FromBody] StockRequestModel stock)
        {
            ProductDTO product = this.productsFacade.Get(stock.ProductId);
            StockDTO stockDto = StockRequestModel.StockRequestModelToStockDTO(stock);
            stockDto.Product = product;
            return stocksFacade.Modify(stockDto);
        }

        /// <summary>
        /// Create a new Stock definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_stock_add")]
        [HttpPost]
        public StockRequestModel Add([FromBody] StockNewRequestModel stock)
        {
            ProductDTO product = this.productsFacade.Get(stock.ProductId);
            StockDTO stockDto = StockNewRequestModel.StockNewRequestModelToStockDTO(stock);
            stockDto.Product = product;
            return StockRequestModel.StockDTOToStockRequestModel(
                stocksFacade.Add(stockDto));
        }

        /// <summary>
        /// Delete a Stock definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_stock_delete")]
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return stocksFacade.Remove(id);
        }
    }
}
