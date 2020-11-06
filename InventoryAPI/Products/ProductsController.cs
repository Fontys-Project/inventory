using InventoryLogic.Products;
using InventoryLogic.Facade;
using InventoryAPI.Products.ViewModels;
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
        private readonly IDomainFactory domainFactory;

        public ProductsController(ProductsFacade productsFacade, IDomainFactory domainFactory)
        {
            this.productsFacade = productsFacade;
            this.domainFactory = domainFactory;
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
        public bool Modify([FromBody] ProductDTO obj)
        {
            return productsFacade.Modify(obj);
        }

        /// <summary>
        /// Create a new Product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public ProductDTO Add([FromBody] AddProductVM vm)
        {
            ProductDTO product = domainFactory.CreateProductDTO();
            product.Name = vm.Name;
            product.Price = vm.Price;
            product.Sku = vm.Sku;
            return productsFacade.Add(product);
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
