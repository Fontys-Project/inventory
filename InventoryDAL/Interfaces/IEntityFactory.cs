using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IEntityFactory
    {
        TagEntity CreateTagEntity();
        StockEntity CreateStockEntity();
        ProductEntity CreateProductEntity();
    }
}