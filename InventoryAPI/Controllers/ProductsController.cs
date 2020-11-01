using InventoryLogic.Products;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class ProductsController : CrudController<ProductDTO>
    {
        public ProductsController(ProductsFacade productsFacade)
            : base((ICrudFacade<ProductDTO>)productsFacade)
        {
        }
    }
}
