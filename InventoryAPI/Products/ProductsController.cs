using InventoryLogic.Products;
using InventoryLogic.Facade;
using InventoryAPI.Products.RequestModels;
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
        public List<ProductRequestModel> GetAll()
        {
            var products = productsFacade.GetAll();

            var productRequestModels = products.ConvertAll(new System.Converter<ProductDTO, ProductRequestModel>(ProductRequestModel.ProductDTOToProductRequestModel));

            return productRequestModels;
        }

        /// <summary>
        /// Get a list of all Product definitions with a specified tag
        /// </summary>
        [HttpGet]
        [Route("filter/{tagId}")]
        public List<ProductRequestModel> GetAll(int tagId)
        {
            var products = productsFacade.GetAll(tagId);

            var productRequestModels = products.ConvertAll(new System.Converter<ProductDTO, ProductRequestModel>(ProductRequestModel.ProductDTOToProductRequestModel));

            return productRequestModels;
        }

        /// <summary>
        /// Get a specified Product definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ProductRequestModel Get(int id)
        {
            return ProductRequestModel.ProductDTOToProductRequestModel(productsFacade.Get(id));
        }

        /// <summary>
        /// Modify a Product definition
        /// </summary>
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_stock_modify")]
        public bool Modify([FromBody] ProductRequestChildModel product)
        {
            return productsFacade.Modify(ProductRequestChildModel.ProductRequestChildModelToProductDTO(product));
        }

        /// <summary>
        /// Create a new Product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_stock_add")]
        [HttpPost]
        public ProductRequestModel Add([FromBody] ProductNewRequestModel product)
        {
            return ProductRequestModel.ProductDTOToProductRequestModel(
                productsFacade.Add(ProductNewRequestModel.ProductNewRequestModelToProductDTO(product)));
        }

        /// <summary>
        /// Delete a Product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_stock_delete")]
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return productsFacade.Remove(id);
        }

        /// <summary>
        /// Apply a tag to a product
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_tag_applytag")]
        // TODO: role aanpassen naar inventory_product_applytag
        [HttpPost]
        [Route("{id}/applytag")]
        public bool ApplyTag(int id, int tagId)
        {
            return productsFacade.ApplyTag(id, tagId);
        }

        /// <summary>
        /// Apply a tag to a product
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_removetag")]
        [HttpPost]
        [Route("{id}/removetag")]
        public bool RemoveTag(int id, int tagId)
        {
            return productsFacade.RemoveTag(id, tagId);
        }
    }
}
