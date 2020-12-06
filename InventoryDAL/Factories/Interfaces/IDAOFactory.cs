using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.ProductTag.Interfaces;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.StockEntities.Interfaces;
using InventoryDAL.Tags;

namespace InventoryDAL.Factories.Interfaces
{
    public interface IDAOFactory
    {
        IProductEntityDAO ProductEntityDAO { get; }
        IStockEntityDAO StockEntityDAO { get; }
        ITagEntityDAO TagEntityDAO { get; }
        IProductTagDAO ProductTagDAO { get; }
        IHasCrudActions<T> GetCrudDAO<T>();
    }
}