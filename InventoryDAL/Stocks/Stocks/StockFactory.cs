using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Stocks.Stocks.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Stocks.Stocks
{
    public class StockFactory : IStockFactory
    {
        public Stock Create(int id, int productId, int amount, DateTime date, Product product)
        {
            return new Stock(id, productId, amount, date, product);
        }
    }
}
