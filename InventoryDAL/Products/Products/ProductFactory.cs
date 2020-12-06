using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System.Collections.Generic;
using InventoryDAL.Products.Products.Interfaces;

namespace InventoryDAL.Products.Products
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(int id, string name, decimal price, string sku, List<Tag> tags, List<Stock> stocks)
        {
            return new Product(id, name, price, sku, tags, stocks);
        }
    }
}
