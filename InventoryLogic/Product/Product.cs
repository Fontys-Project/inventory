using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Products
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }

        // Constructor used by .net API framwork
        public Product()
        {

        }

        public Product(int id, string name, decimal price, string sku)
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
        }
    }



}
