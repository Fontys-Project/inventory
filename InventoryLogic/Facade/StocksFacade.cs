using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public class StocksFacade : CrudFacade<Stock>
    {
        public StocksFacade(IDAOFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}

