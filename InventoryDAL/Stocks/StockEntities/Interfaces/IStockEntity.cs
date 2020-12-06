using System;
using InventoryDAL.Products.ProductEntities;

namespace InventoryDAL.Stocks.StockEntities.Interfaces
{
    public interface IStockEntity
    {
        int Id { get; set; }
        int ProductId { get; set; }
        ProductEntity ProductEntity { get; set; }
        int Amount { get; set; }
        DateTime Date { get; set; }
    }
}
