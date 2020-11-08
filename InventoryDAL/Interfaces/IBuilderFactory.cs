using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Products;
using InventoryLogic.Stocks;

namespace InventoryDAL.Interfaces
{
    public interface IBuilderFactory
    {
        TagBuilder TagConverter { get; }

        ProductBuilder CreateProductBuilder(ProductEntity entity);
        ProductEntityBuilder CreateProductEntityBuilder(Product product);
        StockBuilder CreateStockBuilder(StockEntity stockEntity);
        StockEntityBuilder CreateStockEntityBuilder(Stock stock);
    }
}