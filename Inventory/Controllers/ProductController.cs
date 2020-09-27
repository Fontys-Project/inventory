using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Product;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<IProduct> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Product
            (1, "Koek", 10, "Koek1"))
            .ToArray();
        }
    }
}
