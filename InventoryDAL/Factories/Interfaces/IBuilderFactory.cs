using InventoryDAL.Products;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Factories.Interfaces
{
    public interface IBuilderFactory
    {
        ProductConverter CreateProductBuilder(ProductEntity entity);
        ProductEntityBuilder CreateProductEntityBuilder(Product product);
        StockConverter CreateStockBuilder(StockEntity stockEntity);
        StockEntityBuilder CreateStockEntityBuilder(Stock stock);
        TagBuilder CreateTagBuilder(TagEntity tagEntity);
        TagEntityBuilder CreateTagEntityBuilder(Tag tag);
    }
}