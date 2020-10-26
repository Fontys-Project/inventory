using InventoryLogic.Stocks;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class StocksController : CrudController<Stock>
    {
        public StocksController(StocksFacade stockFacade)
            : base(stockFacade)
        {
        }
    }
}
