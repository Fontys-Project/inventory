using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Factories
{
    public class BuilderFactory : IBuilderFactory
    {
        private readonly IDomainFactory domainFactory;
        private readonly IEntityFactory entityFactory; 
        private readonly IBareModelSupplierFactory bareModelSupplierFactory;

        public BuilderFactory(IDomainFactory domainFactory,
                              IEntityFactory entityFactory,
                              IBareModelSupplierFactory bareModelSupplierFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.bareModelSupplierFactory = bareModelSupplierFactory;
        }

        public ProductConverter CreateProductBuilder(ProductEntity productEntity)
        {
            return new ProductConverter(productEntity,
                                        domainFactory,
                                        bareModelSupplierFactory.GetBareStocksSupplier(daoFactory, builderFactory),
                                        bareModelSupplierFactory.BareTagsSupplier);
        }

        public ProductEntityBuilder CreateProductEntityBuilder(Product product)
        {
            return new ProductEntityBuilder(product, entityFactory, daoFactory);
        }

        public StockConverter CreateStockBuilder(StockEntity stockEntity)
        {
            return new StockBuilder(stockEntity, domainFactory, bareModelSupplierFactory.BareProductsSupplier);
        }

        public StockEntityBuilder CreateStockEntityBuilder(Stock stock)
        {
            return new StockEntityBuilder(stock, entityFactory, daoFactory);
        }

        public TagBuilder CreateTagBuilder(TagEntity tagEntity)
        {
            return new TagBuilder(tagEntity, domainFactory, bareModelSupplierFactory.BareProductsSupplier);
        }

        public TagEntityBuilder CreateTagEntityBuilder(Tag tag)
        {
            return new TagEntityBuilder(tag, entityFactory, daoFactory);
        }
    }
}
