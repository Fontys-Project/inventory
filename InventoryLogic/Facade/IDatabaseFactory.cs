using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;
using InventoryLogic.ProductTags;

namespace InventoryLogic.Facade
{
    public interface IDatabaseFactory
    {
        public IProductDAO GetProductDAO();
        public IProductTagDAO GetProductTagDAO();
    }
}
