using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public class StocksFacade : CrudFacade<Stock>
    {
        public StocksFacade(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}

