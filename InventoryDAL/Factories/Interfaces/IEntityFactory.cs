using InventoryDAL.Products;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Tags;

namespace InventoryDAL.Factories.Interfaces
{
    public interface IEntityFactory
    {
        TagEntity CreateTagEntity();
        StockEntity CreateStockEntity();
        ProductEntity CreateProductEntity();
        ProductTagEntity CreateProductTagEntity(int productId, int tagId, IDAOFactory daoFactory);
    }
}