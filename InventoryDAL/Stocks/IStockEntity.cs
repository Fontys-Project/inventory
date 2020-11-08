using InventoryDAL.Products;
using System;

namespace InventoryDAL.Stocks
{
    public interface IStockEntity
    {
        int Amount { get; set; }
        DateTime Date { get; set; }
        int Id { get; set; }
        ProductEntity ProductEntity { get; set; }
        int ProductId { get; set; }
    }
}