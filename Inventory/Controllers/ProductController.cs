using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Product;
using InventoryLogic.Facade;
using InventoryDI.Database;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private ProductFacade productFacade;

        public ProductController(ProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productFacade.GetProducts();
        }

        [HttpPost]
        public System.Net.Http.HttpResponseMessage Post([FromBody]Product product)
        {
            productFacade.AddProduct(product.Name,product.Id,product.Price,product.Sku);
            return null;
        }

    }


}
