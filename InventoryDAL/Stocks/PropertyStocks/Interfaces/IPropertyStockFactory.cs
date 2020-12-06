using System;

namespace InventoryDAL.Stocks.PropertyStocks.Interfaces
{
    public interface IPropertyStockFactory
    {
        PropertyStock Create(int id, int productId, int amount, DateTime date);
    }
}