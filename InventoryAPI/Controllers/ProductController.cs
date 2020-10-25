using Microsoft.AspNetCore.Mvc;
using InventoryLogic.Products;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class ProductController : APIController<Product>
    {
        public ProductController(ProductFacade productFacade) 
            : base(productFacade)
        {
        }
    }
}
