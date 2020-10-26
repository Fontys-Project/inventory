using InventoryLogic.Products;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class ProductsController : CrudController<Product>
    {
        public ProductsController(ProductsFacade productFacade) 
            : base(productFacade)
        {
        }
    }
}
