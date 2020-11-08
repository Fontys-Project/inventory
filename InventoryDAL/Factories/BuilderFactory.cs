using InventoryDAL.Tags;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Factories
{
    public class BuilderFactory : IBuilderFactory
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IEntityFactory entityFactory; 
        private readonly IDAOFactory daoFactory;

        public BuilderFactory(IDomainFactory domainFactory,
                              IEntityFactory entityFactory,
                              IRepositoryFactory repositoryFactory,
                              IDAOFactory daoFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.repositoryFactory = repositoryFactory;
            this.daoFactory = daoFactory;
        }

        public ProductBuilder CreateProductBuilder(ProductEntity productEntity)
        {
            return new ProductBuilder(productEntity, this.domainFactory, this.repositoryFactory);
        }

        public ProductEntityBuilder CreateProductEntityBuilder(Product product)
        {
            return new ProductEntityBuilder(product, this.entityFactory, this.daoFactory);
        }

        public StockBuilder CreateStockBuilder(StockEntity stockEntity)
        {
            return new StockBuilder(stockEntity, this.domainFactory, this.repositoryFactory);
        }

        public StockEntityBuilder CreateStockEntityBuilder(Stock stock)
        {
            return new StockEntityBuilder(stock, this.entityFactory, this.daoFactory);
        }

        public TagBuilder CreateTagBuilder(TagEntity tagEntity)
        {
            return new TagBuilder(tagEntity, this.domainFactory, this.repositoryFactory);
        }

        public TagEntityBuilder CreateTagEntityBuilder(Tag tag)
        {
            return new TagEntityBuilder(tag, this.entityFactory, this.daoFactory);
        }
    }
}
