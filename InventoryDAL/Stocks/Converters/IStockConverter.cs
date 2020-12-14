using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks
{
    public interface IStockConverter
    {
        public delegate void OnObjectCreation(Stock stock, IStockEntity stockEntity);
        public Stock Convert(IStockEntity stockEntity, OnObjectCreation onObjectCreation);
    }
}
