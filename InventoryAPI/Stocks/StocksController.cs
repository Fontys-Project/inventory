using InventoryLogic.Stocks;
using InventoryLogic.Facade;
using InventoryAPI.Crud;
using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Stocks.RequestModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InventoryAPI.Stocks
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class StocksController : ControllerBase
    {
        private readonly StocksFacade stocksFacade;

        public StocksController(StocksFacade stocksFacade)
        {
            this.stocksFacade = stocksFacade;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_modify")]
        public bool Modify([FromBody] StockRequestModel stock)
        {
            return stocksFacade.Modify(StockRequestModel.StockRequestModelToStockDTO(stock));
        }

        /// <summary>
        /// Create a new Stock definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_add")]
        [HttpPost]
        public StockRequestModel Add([FromBody] StockNewRequestModel stock)
        {
            return StockRequestModel.StockDTOToStockRequestModel(
                stocksFacade.Add(StockNewRequestModel.StockNewRequestModelToStockDTO(stock)));
        }

        /// <summary>
        /// Delete a Stock definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_delete")]
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return stocksFacade.Remove(id);
        }
    }
}
