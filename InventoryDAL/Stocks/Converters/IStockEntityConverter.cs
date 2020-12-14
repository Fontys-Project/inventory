using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks
{
    public interface IStockEntityConverter
    {
        public StockEntity Convert(Stock stock);
    }
}