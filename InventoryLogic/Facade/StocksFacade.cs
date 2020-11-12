using InventoryLogic.Interfaces;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public class StocksFacade : CrudFacade<Stock>
    {
        public StocksFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }
    }
}

