using System;
using System.Collections.Generic;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Factories.Interfaces
{
    public interface IDomainFactory
    {
        Tag CreateTag(int id, string name, List<Product> products);
        Stock CreateStock(int id, int productId, int amount, DateTime date, Product product = null);
        Product CreateProduct(int id, string name, decimal price, string sku, List<Tag> tags, List<Stock> stocks);
    }
}