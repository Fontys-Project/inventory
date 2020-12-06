using System;
using InventoryLogic.Products;
using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks.Stocks.Interfaces
{
    public interface IStockFactory
    {
        Stock Create(int id, int productId, int amount, DateTime date, Product product);
    }
}