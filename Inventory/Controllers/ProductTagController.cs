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
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productFacade.GetProducts();
        }

        /// <summary>
        /// Get product
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public Product Get(int id)
        {
                return productFacade.GetProduct(id);
        }

        /// <summary>
        /// Modify a product
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        public Boolean Modify([FromBody]Product product,int id)
        {
            Product curProduct = productFacade.GetProduct(id);
            curProduct.Name = product.Name;
            curProduct.Price = product.Price;
            curProduct.Sku = product.Sku;
            return productFacade.ModifyProduct(curProduct);
        }


        /// <summary>
        /// Create a new product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public Product Put([FromBody]Product product)
        {
            return productFacade.AddProduct(product.Name, product.Id, product.Price, product.Sku);
        }

        /// <summary>
        /// Deletes a product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{id}")]
        public Boolean Delete(int id)
        {
            return productFacade.RemoveProduct(id);
        }

    }


}
