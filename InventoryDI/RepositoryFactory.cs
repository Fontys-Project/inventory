using InventoryDAL.Database;
using InventoryDAL.Factories;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDI
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IProductsRepository ProductsRepository { get; }
        public IStocksRepository StocksRepository { get; }
        public ITagsRepository TagsRepository { get; }

        public RepositoryFactory()
        {
            var daoFactory = new DAOFactory(DatabaseType.MYSQL);
            var builderFactory = new BuilderFactory(new DomainFactory(),
                                                    new EntityFactory(),
                                                    this,
                                                    daoFactory);

            ProductsRepository = new ProductsRepository(daoFactory.ProductEntityDAO, builderFactory);
            StocksRepository = new StocksRepository(daoFactory.StockEntityDAO, builderFactory);
            TagsRepository = new TagsRepository(daoFactory.TagEntityDAO, builderFactory);
        }

        public ICrudRepository<T> GetCrudRepository<T>()
        {
            // Just for crud actions. If you need more actions from your repository 
            // you will need to call property directly

            if (typeof(T) == typeof(Product))
                return (ICrudRepository<T>)ProductsRepository;
            if (typeof(T) == typeof(Stock))
                return (ICrudRepository<T>)StocksRepository;
            if (typeof(T) == typeof(Tag))
                return (ICrudRepository<T>)TagsRepository;
            return null;
        }
    }
}
