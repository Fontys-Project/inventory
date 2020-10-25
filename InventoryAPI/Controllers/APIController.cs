using System;
using System.Collections.Generic;
using InventoryLogic.Facade;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public abstract class APIController<Type> : ControllerBase
    {
        private readonly IFacade<Type> facade;

        public APIController(IFacade<Type> facade)
        {
            this.facade = facade;
        }

        /// <summary>
        /// List of <typeparamref name="Type"/> definitions
        /// </summary>
        [HttpGet]
        public IEnumerable<Type> Get()
        {
            return facade.GetAll();
        }

        /// <summary>
        /// Get <typeparamref name="Type"/>
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public Type Get(int id)
        {
            return facade.Get(id);
        }

        /// <summary>
        /// Modify a <typeparamref name="Type"/>
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        public Boolean Modify([FromBody] Type obj)
        {
            return facade.Modify(obj);
        }


        /// <summary>
        /// Create a new <typeparamref name="Type"/> definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public Type Put([FromBody] Type obj)
        {
            return facade.Add(obj);
        }

        /// <summary>
        /// Deletes a <typeparamref name="Type"/> definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{id}")]
        public Boolean Delete(int id)
        {
            return facade.Remove(id);
        }
    }
}

