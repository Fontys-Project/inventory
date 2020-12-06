using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Database;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Products.PropertyProducts;
using InventoryDAL.Products.PropertyProducts.Interfaces;
using InventoryDAL.Stocks;
using InventoryDAL.Stocks.PropertyStocks;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;
using InventoryDAL.Tags;
using InventoryDAL.Tags.Repository;

namespace InventoryDAL.Factories
{
    public class BareModelSupplierFactory : IBareModelSupplierFactory
    {
        public IPropertyProductSupplier GetBareProductsSupplier(IDAOFactory daoFactory, IBuilderFactory builderFactory)
        {
            return new PropertyProductSupplier(daoFactory.ProductEntityDAO, builderFactory);
        }

        public IPropertyStockSupplier GetBareStocksSupplier(IDAOFactory daoFactory, IBuilderFactory builderFactory)
        {
            return new PropertyStockSupplier(daoFactory.StockEntityDAO, builderFactory);
        }

        public IPropertyTagSupplier GetBareTagsSupplier(IDAOFactory daoFactory, IBuilderFactory builderFactory)
        {
            return new PropertyTagSupplier(daoFactory.TagEntityDAO, builderFactory);
        }
    }
}
