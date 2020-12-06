using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks.Stocks.Interfaces
{
    public interface IStockConverter : IStock
    {
        Stock GetResult();
    }
}
