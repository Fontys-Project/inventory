using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks
{
    public interface IStockEntityBuilder : IStockEntity
    {
        StockEntity Build();
    }
}