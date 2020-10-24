using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;

namespace InventoryLogic.Stocks
{
    public class Stock
    {

        public int Id { get; }
        public string Productname { get; set; }

        public int Amount { get; set; }

        public DateTime Today { get; set; }

        // Constructor used by .net API framwork
        public Stock()
        {

        }

        public Stock(int id, string productname, int amount)
        {
            Id = id;
            Productname = productname;
            Amount = amount;
            Today = DateTime.Today;
        }
    }



}
