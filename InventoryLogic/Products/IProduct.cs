using InventoryLogic.ProductTag;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Products
{
    interface IProduct
    {
        public int Id { get;  }
        public string Name { get;  }
        public decimal Price { get;  }
        public string Sku { get;  }
        public List<Tag> Tags { get; }
        public List<Stock> Stocks { get; }



    }
}
