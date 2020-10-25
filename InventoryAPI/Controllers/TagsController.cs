using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Facade;
using InventoryLogic.Tags;

namespace InventoryAPI.Controllers
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class TagsController : APIController<Tag>
    {
        public TagsController(ProductTagsFacade tagsFacade)
            : base(tagsFacade)
        {
        }
    }
}
