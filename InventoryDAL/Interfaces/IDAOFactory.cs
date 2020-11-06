using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IDAOFactory
    {
        IProductEntityDAO ProductEntityDAO { get; }
        IStockEntityDAO StockEntityDAO { get; }
        ITagEntityDAO TagEntityDAO { get; }
        IProductTagDAO ProductTagDAO { get; }
        ICrudDAO<T> GetCrudDAO<T>();
    }
}