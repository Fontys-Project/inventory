using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;

namespace InventoryLogic.Facade
{
    public interface IDatabaseFactory
    {
        public IProductDAO GetProductDAO();
    }
}
