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
        /// List of all Tag definitions
        /// </summary>
        [HttpGet]
        public List<TagRequestModel> GetAll()
        {
            var tags = tagsFacade.GetAll();

            var stockRequestModels = tags.ConvertAll(new System.Converter<TagDTO, TagRequestModel>(TagRequestModel.TagDTOToTagRequestModel));

            return stockRequestModels;
        }

        /// <summary>
        /// Get a specified Tag definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public TagRequestModel Get(int id)
        {
            TagDTO tag = tagsFacade.Get(id);
            return TagRequestModel.TagDTOToTagRequestModel(tag);
        }

        /// <summary>
        /// Create a new Tag definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public TagRequestModel Add([FromBody] AddTagRequestModel tag)
        {
            TagDTO dto = tagsFacade.Add(AddTagRequestModel.AddTagRequestModelToTagDTO(tag));
            return TagRequestModel.TagDTOToTagRequestModel(dto);
        }

        /// <summary>
        /// Modify a Tag definition
        /// </summary>
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_tag_modify")]
        public bool Modify([FromBody] TagRequestModel tag)
        {
            return tagsFacade.Modify(TagRequestModel.TagRequestModelToTagDTO(tag));
        }


        /// <summary>
        /// Delete a Tag definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "inventory_tag_delete")]
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return tagsFacade.Remove(id);
        }
    }
}
