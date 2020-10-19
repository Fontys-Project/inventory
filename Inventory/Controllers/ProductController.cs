using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Product;
using InventoryLogic.Facade;
using InventoryDI.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InventoryAPI.Controllers
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class ProductController : ControllerBase
    {

        private ProductFacade productFacade;

        public ProductController(ProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }

        /// <summary>
        /// List of product definitions
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productFacade.GetProducts();
        }


        /// <summary>
        /// Create a new product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [MapToApiVersion("0.2")]
        [HttpPut]
        public Product Put([FromBody]Product product)
        {
            return productFacade.AddProduct(product.Name, product.Id, product.Price, product.Sku); ;
        }

    }


}
