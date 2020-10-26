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
    public abstract class CrudController<Type> : ControllerBase
    {
        protected readonly CrudFacade<Type> facade;

        public CrudController(CrudFacade<Type> facade)
        {
            this.facade = facade;
        }

        /// <summary>
        /// List of all <typeparamref name="Type"/> definitions
        /// </summary>
        [HttpGet]
        public IEnumerable<Type> GetAll()
        {
            return facade.GetAll();
        }

        /// <summary>
        /// Get a specified <typeparamref name="Type"/> definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public Type Get(int id)
        {
            return facade.Get(id);
        }

        /// <summary>
        /// Modify a <typeparamref name="Type"/> definition
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        /// Delete a <typeparamref name="Type"/> definition
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

