using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IEntityFactory
    {
        TagEntity CreateTagEntity();
        StockEntity CreateStockEntity();
        ProductEntity CreateProductEntity();
        ProductTagEntity CreateProductTagEntity();
    }
}