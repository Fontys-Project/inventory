using System;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.PropertyProducts;
using InventoryDAL.Products.PropertyProducts.Interfaces;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.StockEntities.Interfaces;
using InventoryDAL.Stocks.Stocks.Interfaces;
using InventoryDAL.Tags.Repository;
using InventoryLogic.Products;
using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks.Stocks
{
    public class StockConverter : IStockConverter
    {
        private readonly IStockFactory stockFactory;
        private readonly IPropertyProductSupplier propertyProductSupplier;

        public StockConverter(IStockFactory stockFactory, IPropertyProductSupplier propertyProductSupplier)
        {
            this.stockFactory = stockFactory;
            this.propertyProductSupplier = propertyProductSupplier;
        }

        public Stock Convert(IStockEntity stockEntity)
        {
            return stockFactory.Create(stockEntity.Id,
                                        stockEntity.ProductId,
                                        stockEntity.Amount,
                                        stockEntity.Date,
                                        BuildProduct(stockEntity)
                );
        }

        private Product BuildProduct(IStockEntity stockEntity)
        {
            try
            {
                return propertyProductSupplier.Get(stockEntity.ProductId);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Product not found. Please first create the product.", e);
            }
        }
    }
}

