using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public interface IDatabaseFactory
    {
        public IProductDAO ProductDAO { get; }
        public ITagDAO TagDAO { get; }
        public IStockDAO StockDAO { get; }
        public ICrudDAO<T> GetCrudDAO<T>();
    }
}
