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
using InventoryLogic.ProductTags;

namespace InventoryAPI.Controllers
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class ProductTagsController : ControllerBase
    {

        private ProductTagsFacade tagsFacade;

        public ProductTagsController(ProductTagsFacade tagsFacade)
        {
            this.tagsFacade = tagsFacade;
        }

        /// <summary>
        /// List of product definitions
        /// </summary>
        [HttpGet]
        public IEnumerable<ProductTag> Get()
        {
            return tagsFacade.Get();
        }

        /// <summary>
        /// Get product
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ProductTag Get(int id)
        {
                return tagsFacade.Get(id);
        }

        /// <summary>
        /// Modify a product
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        public Boolean Modify([FromBody]ProductTag tag, int id)
        {
            ProductTag curTag = tagsFacade.Get(id);
            curTag.Name = tag.Name;
            return tagsFacade.Modify(curTag);
        }


        /// <summary>
        /// Create a new product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public Product Put([FromBody]ProductTag tag)
        {
            return tagsFacade.Add(tag.Name, tag.Id);
        }

        /// <summary>
        /// Deletes a product definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{id}")]
        public Boolean Delete(int id)
        {
            return tagsFacade.Remove(id);
        }

    }


}
