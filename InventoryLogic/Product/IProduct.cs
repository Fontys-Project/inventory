using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Product
{
    public interface IProduct
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public string Sku { get; }

    }
}
