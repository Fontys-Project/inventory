using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;

namespace InventoryDAL.Stocks.PropertyStocks
{
    class PropertyStockFactory : IPropertyStockFactory
    {
        public PropertyStock Create(int id, int productId, int amount, DateTime date)
        {
            return new PropertyStock(id, productId, amount, date);
        }
    }
}