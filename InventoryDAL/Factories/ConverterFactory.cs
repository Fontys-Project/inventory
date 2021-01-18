using InventoryDAL.Tags;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryLogic.Interfaces;

namespace InventoryDAL.Factories
{
    public class ConverterFactory : IConverterFactory
    {
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
            productConverter = new ProductConverter(domainFactory, repositoryFactory);
            productEntityConverter = new ProductEntityConverter(entityFactory, daoFactory);
            stockConverter = new StockConverter(domainFactory, repositoryFactory);
            stockEntityConverter = new StockEntityConverter(entityFactory, daoFactory);
            tagConverter = new TagConverter(domainFactory, repositoryFactory);
            tagEntityConverter = new TagEntityConverter(entityFactory, daoFactory);
        }
    }
}
