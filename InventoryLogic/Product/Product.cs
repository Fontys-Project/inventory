using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Product
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public string Sku { get; private set; }

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
