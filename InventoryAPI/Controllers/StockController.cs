using InventoryLogic.Stocks;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class StockController : APIController<Stock>
    {
        public StockController(StockFacade stockFacade)
            : base(stockFacade)
        {
        }
    }
}
