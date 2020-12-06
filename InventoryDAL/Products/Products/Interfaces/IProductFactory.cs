using System.Collections.Generic;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Products.Products.Interfaces
{
    public interface IProductFactory
    {
        Product Create(int id, string name, decimal price, string sku, List<Tag> tags, List<Stock> stocks);
    }
}