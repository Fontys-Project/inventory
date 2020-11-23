using InventoryLogic.Interfaces;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public class StocksFacade : CrudDTOFacade<Stock, StockDTO>
    {
        public StocksFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }
    }
}

