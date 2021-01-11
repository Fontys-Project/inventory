using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IConverterFactory
    {
        public ProductConverter productConverter { get; }
        public ProductEntityConverter productEntityConverter { get; }
        public StockConverter stockConverter { get; }
        public StockEntityConverter stockEntityConverter { get; }
        public TagConverter tagConverter { get; }
        public TagEntityConverter tagEntityConverter { get; }

    }
}