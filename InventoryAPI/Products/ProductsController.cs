using InventoryLogic.Products;
using InventoryLogic.Facade;
using InventoryAPI.Products.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using InventoryDAL.Interfaces;
using System.Collections.Generic;

namespace InventoryAPI.Products
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsFacade productsFacade;

        public ProductsController(ProductsFacade productsFacade)
        {
            this.productsFacade = productsFacade;
        }

        /// <summary>
        /// List of all Product definitions
        /// </summary>
        [HttpGet]
        public List<ProductDTO> GetAll()
        {
            return productsFacade.GetAll();
        }

        /// <summary>
        /// Get a specified Product definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ProductDTO Get(int id)
        {
            return productsFacade.Get(id);
        }

        /// <summary>
        /// Modify a Product definition
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool Modify([FromBody] ProductDTO productDTO)
        {
            return productsFacade.Modify(productDTO);
        }

        /// <summary>
        /// Create a new Product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_add")]
        [HttpPut]
        public ProductDTO Add([FromBody] AddProductAM vm)
        {
            ProductDTO productDto = new ProductDTO // TODO: is newing ok?
            {
                Name = vm.Name,
                Price = vm.Price,
                Sku = vm.Sku
            }; 
            return productsFacade.Add(productDto);
        }

        /// <summary>
        /// Delete a Product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return productsFacade.Remove(id);
        }
    }
}
