using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System.Collections.Generic;

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
