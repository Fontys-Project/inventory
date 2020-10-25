﻿using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Stocks;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class StockController : APIController<Stock>
    {
        public StockController(StockFacade stockFacade)
            : base(stockFacade)
        {
        }
    }
}
