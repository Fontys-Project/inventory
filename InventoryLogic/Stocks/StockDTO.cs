using InventoryLogic.Products;
using System;
using InventoryLogic.Interfaces;

namespace InventoryLogic.Stocks
{
    public class StockDTO : IHasUniqueObjectId
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
