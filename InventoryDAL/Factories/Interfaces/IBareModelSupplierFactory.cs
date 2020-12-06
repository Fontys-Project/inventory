using InventoryDAL.Products.PropertyProducts;
using InventoryDAL.Products.PropertyProducts.Interfaces;
using InventoryDAL.Stocks.PropertyStocks;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;
using InventoryDAL.Tags.Repository;

namespace InventoryDAL.Factories.Interfaces
{
    public interface IBareModelSupplierFactory
    {
        IPropertyProductSupplier GetBareProductsSupplier(IDAOFactory daoFactory, IBuilderFactory builderFactory);
        IPropertyStockSupplier GetBareStocksSupplier(IDAOFactory daoFactory, IBuilderFactory builderFactory);
        IPropertyTagSupplier GetBareTagsSupplier(IDAOFactory daoFactory, IBuilderFactory builderFactory);
    }
}