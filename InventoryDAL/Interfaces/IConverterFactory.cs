using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IConverterFactory
    {
        ProductConverter ProductConverter { get; }
        StockConverter StockConverter { get; }
        TagConverter TagConverter { get; }
    }
}