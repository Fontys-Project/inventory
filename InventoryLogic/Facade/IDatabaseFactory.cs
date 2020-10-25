using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public interface IDatabaseFactory
    {
        public IProductDAO GetProductDAO();
        public ITagDAO GetTagDAO();
        public IStockDAO GetStockDAO();
    }
}
