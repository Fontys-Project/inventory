using InventoryDAL.Stocks.StockEntities.Interfaces;

namespace InventoryDAL.Stocks.PropertyStocks.Interfaces
{
    public interface IPropertyStockConverter
    {
        PropertyStock Convert(IStockEntity stockEntity);
    }
}