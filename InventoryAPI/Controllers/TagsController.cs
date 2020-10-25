using InventoryLogic.Facade;
using InventoryLogic.Tags;

namespace InventoryAPI.Controllers
{
    public class TagsController : APIController<Tag>
    {
        public TagsController(ProductTagsFacade tagsFacade)
            : base(tagsFacade)
        {
        }
    }
}
