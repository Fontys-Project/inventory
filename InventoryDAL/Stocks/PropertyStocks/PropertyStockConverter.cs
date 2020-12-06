using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.Products.PropertyProducts;
using InventoryDAL.Products.PropertyProducts.Interfaces;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;
using InventoryDAL.Stocks.StockEntities.Interfaces;

namespace InventoryDAL.Stocks.PropertyStocks
{
    public class PropertyStockConverter : IPropertyStockConverter
    {
        private readonly IPropertyStockFactory propertyStockFactory;

        public PropertyStockConverter(IPropertyStockFactory propertyStockFactory)
        {
            this.propertyStockFactory = propertyStockFactory;
        }

        public PropertyStock Convert(IStockEntity stockEntity)
        {

            return propertyStockFactory.Create(stockEntity.Id,
                                                stockEntity.ProductId,
                                                stockEntity.Amount,
                                                stockEntity.Date);
        }
    }
}
