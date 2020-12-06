using System;

namespace InventoryDAL.Stocks.PropertyStocks.Interfaces
{
    public interface IPropertyStock
    {
        int Id { get; }
        int ProductId { get; }
        int Amount { get; }
        DateTime Date { get; }
    }
}