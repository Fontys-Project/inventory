using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks
{
    public interface IStockConverter
    {
        Stock ConvertToStock(StockEntity e);
        StockEntity ConvertToStockEntity(Stock stock);
    }
}