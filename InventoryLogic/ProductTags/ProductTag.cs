using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.ProductTags
{
    public class ProductTag
    {
        
        public int Id { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }

        // Constructor used by .net API framwork
        public ProductTag()
        {

        }

        public ProductTag(int id, string name, decimal price, string sku)
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
        }
    }



}
