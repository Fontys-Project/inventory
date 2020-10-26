using InventoryLogic.Facade;
using InventoryLogic.Tags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InventoryAPI.Controllers
{
    public class TagsController : CrudController<Tag>
    {
        public TagsController(TagsFacade tagsFacade)
            : base(tagsFacade)
        {
        }

        /// <summary>
        /// Apply a tag to a product
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{productId}")]
        public Boolean ApplyTag(int productId, int tagId)
        {
            return facade.ApplyTag(productId, tagId);
        }
    }
}
