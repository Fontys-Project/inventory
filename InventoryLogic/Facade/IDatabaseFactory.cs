using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;
using InventoryLogic.ProductTags;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public interface IDatabaseFactory
    {
        public IProductDAO GetProductDAO();
        public IProductTagDAO GetProductTagDAO();

        public IStockDAO GetStockDAO();
    }
}
