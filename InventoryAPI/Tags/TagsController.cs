using InventoryAPI.Crud;
using InventoryLogic.Facade;
using InventoryLogic.Tags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InventoryAPI.Tags
{
    public class TagsController : CrudController<Tag>
    {
        private readonly TagsFacade tagsFacade;
        
        public TagsController(TagsFacade tagsFacade)
            : base((ICrudFacade<Tag>)tagsFacade)
        {
            this.tagsFacade = tagsFacade;
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
