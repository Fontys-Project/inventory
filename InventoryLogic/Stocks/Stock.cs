using System;
using InventoryLogic.Products;

namespace InventoryLogic.Stocks
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Constructor used by .net API framwork
        public Stock()
        {

        }

        public Stock(int id, Product product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
            Date = DateTime.Today;
        }
    }
}
