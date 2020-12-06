using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
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
