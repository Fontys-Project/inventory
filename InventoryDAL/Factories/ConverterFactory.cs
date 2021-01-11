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
    public class ConverterFactory : IConverterFactory
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IEntityFactory entityFactory; 
        private readonly IDAOFactory daoFactory;
        public ProductConverter productConverter { get; private set; }
        public ProductEntityConverter productEntityConverter { get; private set; }
        public StockConverter stockConverter { get; private set; }
        public StockEntityConverter stockEntityConverter { get; private set; }
        public TagConverter tagConverter { get; private set; }
        public TagEntityConverter tagEntityConverter { get; private set; }

        public ConverterFactory(IDomainFactory domainFactory,
                              IEntityFactory entityFactory,
                              IRepositoryFactory repositoryFactory,
                              IDAOFactory daoFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.repositoryFactory = repositoryFactory;
            this.daoFactory = daoFactory;
            productConverter = new ProductConverter(this.domainFactory, this.repositoryFactory);
            productEntityConverter = new ProductEntityConverter(entityFactory, daoFactory);
            stockConverter = new StockConverter(domainFactory, repositoryFactory);
            stockEntityConverter = new StockEntityConverter(entityFactory, daoFactory);
            tagConverter = new TagConverter(domainFactory, repositoryFactory);
            tagEntityConverter = new TagEntityConverter(entityFactory, daoFactory);

        }

    }
}
