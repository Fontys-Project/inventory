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
        string Sku { get; set; }
        IList<ProductTagEntity> ProductTagEntities { get; set; }
        IList<StockEntity> StockEntities { get; set; }
    }
}