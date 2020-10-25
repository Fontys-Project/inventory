using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Facade;
using InventoryDI.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
