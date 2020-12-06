using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Tags;

namespace InventoryDAL.Factories
{
    public class EntityFactory : IEntityFactory
    {
        public ProductEntity CreateProductEntity()
        {
            return new ProductEntity();
        }

        public StockEntity CreateStockEntity()
        {
            return new StockEntity();
        }

        public TagEntity CreateTagEntity()
        {
            return new TagEntity();
        }

        public ProductTagEntity CreateProductTagEntity(int productId, int tagId, IDAOFactory daoFactory)
        {
            return new ProductTagEntity(productId, tagId, daoFactory);
        }
    }
}
