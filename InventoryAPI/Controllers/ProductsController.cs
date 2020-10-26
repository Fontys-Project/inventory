using InventoryLogic.Products;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class ProductsController : CrudController<Product>
    {
        public ProductsController(ProductsFacade productsFacade)
            : base((ICrudFacade<Product>)productsFacade)
        {
        }
    }
}
