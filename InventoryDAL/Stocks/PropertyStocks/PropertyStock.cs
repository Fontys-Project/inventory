using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;

namespace InventoryDAL.Stocks.PropertyStocks
{
    public class PropertyStock : IPropertyStock
    {
        public int Id { get; }
        public int ProductId { get; }
        public int Amount { get; }
        public DateTime Date { get; }

        public PropertyStock(int id, int productId, int amount, DateTime date)
        {
            Id = id;
            ProductId = productId;
            Amount = amount;
            Date = date;
        }
    }
}
