using InventoryLogic.Products;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class ProductsController : APIController<Product>
    {
        public ProductsController(ProductsFacade productFacade) 
            : base(productFacade)
        {
        }
    }
}
