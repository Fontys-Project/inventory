using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using System.Collections.Generic;

namespace InventoryDAL.Products
{
    public interface IProductEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        List<ProductTagEntity> ProductTagEntities { get; set; }
        string Sku { get; set; }
        List<StockEntity> StockEntities { get; set; }
    }
}