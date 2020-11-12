using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IBuilderFactory
    {
        ProductBuilder CreateProductBuilder(ProductEntity entity);
        ProductEntityBuilder CreateProductEntityBuilder(Product product);
        StockBuilder CreateStockBuilder(StockEntity stockEntity);
        StockEntityBuilder CreateStockEntityBuilder(Stock stock);
        TagBuilder CreateTagBuilder(TagEntity tagEntity);
        TagEntityBuilder CreateTagEntityBuilder(Tag tag);
    }
}