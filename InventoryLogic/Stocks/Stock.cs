using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using InventoryLogic.Products;

namespace InventoryLogic.Stocks
{
    public class Stock
    {

        public int Id { get; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Amount { get; set; }

        public DateTime Today { get; set; }

        // Constructor used by .net API framwork
        public Stock()
        {

        }

        public Stock(int id, Product product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
            Today = DateTime.Today;
        }
    }



}
