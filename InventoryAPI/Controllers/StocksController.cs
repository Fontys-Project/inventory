using InventoryLogic.Stocks;
using InventoryLogic.Facade;

namespace InventoryAPI.Controllers
{
    public class StocksController : CrudController<Stock>
    {
        public StocksController(StocksFacade stocksFacade)
           : base((ICrudFacade<Stock>)stocksFacade)
        {
        }
    }
}
