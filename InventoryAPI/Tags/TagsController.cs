using InventoryAPI.Crud;
using InventoryAPI.Tags.RequestModels;
using InventoryLogic.Facade;
using InventoryLogic.Tags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace InventoryAPI.Tags
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class TagsController : ControllerBase
    {
        private readonly TagsFacade tagsFacade;

        public TagsController(TagsFacade tagsFacade)
        {
            this.tagsFacade = tagsFacade;
        }

        /// <summary>
        /// List of all Stock definitions
        /// </summary>
        [HttpGet]
        public List<TagRequestModel> GetAll()
        {
            var tags = tagsFacade.GetAll();

            var stockRequestModels = tags.ConvertAll(new System.Converter<TagDTO, TagRequestModel>(TagRequestModel.TagDTOToTagRequestModel));

            return stockRequestModels;
        }

        /// <summary>
        /// Get a specified Stock definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public TagRequestModel Get(int id)
        {
            return TagRequestModel.TagDTOToTagRequestModel(tagsFacade.Get(id));
        }

        /// <summary>
        /// Modify a Stock definition
        /// </summary>
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_modify")]
        public bool Modify([FromBody] TagRequestModel tag)
        {
            return tagsFacade.Modify(TagRequestModel.TagRequestModelToTagDTO(tag));
        }

       
        /// <summary>
        /// Delete a Stock definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_product_delete")]
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return tagsFacade.Remove(id);
        }
        /// <summary>
        /// Apply a tag to a product
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{productId}")]
        public Boolean ApplyTag(int productId, int tagId)
        {
            return tagsFacade.ApplyTag(productId, tagId);
        }
    }
}
