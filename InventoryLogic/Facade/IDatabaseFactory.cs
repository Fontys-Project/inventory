using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;
using InventoryLogic.Crud;
using InventoryLogic.ProductTagJoins;

namespace InventoryLogic.Facade
{
    public interface IDatabaseFactory
    {
        public ICrudDAO<Product> ProductDAO { get; }
        public ICrudDAO<Tag> TagDAO { get; }
        public ICrudDAO<Stock> StockDAO { get; }
        public ICrudDAO<T> GetCrudDAO<T>();
        //public ICrudDAO<ProductTagJoin> ProductTagJoinDAO { get; }
    }
}
