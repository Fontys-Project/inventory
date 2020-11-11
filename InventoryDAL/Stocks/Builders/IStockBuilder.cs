using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks
{
    public interface IStockBuilder : IStock
    {
        Stock GetResult();
    }
}
