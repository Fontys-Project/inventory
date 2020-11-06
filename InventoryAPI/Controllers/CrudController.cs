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
        protected readonly ICrudFacade<Type> crudFacade;

        public CrudController(ICrudFacade<Type> crudFacade)
        {
            this.crudFacade = crudFacade;
        }

        /// <summary>
        /// List of all <typeparamref name="Type"/> definitions
        /// </summary>
        [HttpGet]
        public IEnumerable<Type> GetAll()
        {
            return crudFacade.GetAll();
        }

        /// <summary>
        /// Get a specified <typeparamref name="Type"/> definition
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public Type Get(int id)
        {
            return crudFacade.Get(id);
        }

        /// <summary>
        /// Modify a <typeparamref name="Type"/> definition
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Boolean Modify([FromBody] Type obj)
        {
            return crudFacade.Modify(obj);
        }


        /// <summary>
        /// Create a new <typeparamref name="Type"/> definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public Type Add([FromBody] Type obj)
        {
            return crudFacade.Add(obj);
        }

        /// <summary>
        /// Delete a <typeparamref name="Type"/> definition
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{id}")]
        public Boolean Delete(int id)
        {
            return crudFacade.Remove(id);
        }
    }
}

