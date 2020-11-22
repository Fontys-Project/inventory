using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;

namespace InventoryDAL.Interfaces
{
    public interface IDomainFactory
    {
        Tag CreateTag(int id, string name, List<Product> products);
        Stock CreateStock(int id, int productId, Product product, int amount, DateTime date);
        Product CreateProduct(int id, string name, decimal price, string sku, List<Tag> tags, List<Stock> stocks);
    }
}