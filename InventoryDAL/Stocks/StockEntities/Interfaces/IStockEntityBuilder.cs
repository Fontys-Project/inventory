namespace InventoryDAL.Stocks.StockEntities.Interfaces
{
    public interface IStockEntityBuilder : IStockEntity
    {
        StockEntity GetResult();
    }
}