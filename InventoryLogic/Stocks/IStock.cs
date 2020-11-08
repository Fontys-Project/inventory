using InventoryLogic.Products;
using System;

namespace InventoryLogic.Stocks
{
    public interface IStock
    {
        int Amount { get; set; }
        DateTime Date { get; set; }
        int Id { get; set; }
        Product Product { get; set; }
        int ProductId { get; set; }
    }
}