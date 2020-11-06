using InventoryLogic.Stocks;
using InventoryLogic.Facade;
using InventoryAPI.Crud;

namespace InventoryAPI.Stocks
{
    public class StocksController : CrudController<Stock>
    {
        public StocksController(StocksFacade stocksFacade)
           : base((ICrudFacade<Stock>)stocksFacade)
        {
        }
    }
}
