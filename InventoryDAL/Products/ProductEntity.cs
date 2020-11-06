using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using System.Collections.Generic;

namespace InventoryDAL.Products
{
    public class ProductEntity
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public string Sku { get; set; }
        public List<ProductTagEntity> ProductTagEntities { get; set; }
        public List<StockEntity> StockEntities { get; set; }

        public ProductEntity()
        {
            this.ProductTagEntities = new List<ProductTagEntity>();
            this.StockEntities = new List<StockEntity>();
        }
    }
}
