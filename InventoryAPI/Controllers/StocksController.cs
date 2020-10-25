using InventoryLogic.Stocks;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class StocksController : APIController<Stock>
    {
        public StocksController(StocksFacade stockFacade)
            : base(stockFacade)
        {
        }
    }
}
