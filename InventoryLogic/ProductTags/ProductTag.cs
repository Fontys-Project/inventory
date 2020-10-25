using InventoryLogic.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.ProductTags
{
    public class ProductTag
    {
        public int Id { get; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        // Constructor used by .net API framwork
        public ProductTag()
        {

        }

        public ProductTag(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }



}
