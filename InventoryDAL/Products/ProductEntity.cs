using InventoryDAL.Database;
using InventoryDAL.ProductTag;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Facade;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
    }
}
