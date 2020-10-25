using InventoryLogic.Products;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class ProductController : APIController<Product>
    {
        public ProductController(ProductFacade productFacade) 
            : base(productFacade)
        {
        }
    }
}
