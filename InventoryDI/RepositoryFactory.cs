using InventoryDAL.Database;
using InventoryDAL.Factories;
using InventoryDAL.Products;
using InventoryDAL.Products.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.Stocks;
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
            var builderFactory = new BuilderFactory(new DomainModelFactory(),
                                                    new EntityFactory(),
                                                    new BareModelSupplierFactory());

            ProductsRepository = new ProductsRepository(daoFactory, builderFactory);
            StocksRepository = new StocksRepository(daoFactory, builderFactory);
            TagsRepository = new TagsRepository(daoFactory, builderFactory);
        }

        public IHasCrudActions<T> GetCrudRepository<T>()
        {
            // Just for crud actions. If you need more actions from your repository 
            // you will need to call property directly

            if (typeof(T) == typeof(Product))
                return (IHasCrudActions<T>)ProductsRepository;
            if (typeof(T) == typeof(Stock))
                return (IHasCrudActions<T>)StocksRepository;
            if (typeof(T) == typeof(Tag))
                return (IHasCrudActions<T>)TagsRepository;
            return null;
        }


    }
}
