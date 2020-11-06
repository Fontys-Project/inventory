using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryDI
{
    public class EntityFactory : IEntityFactory
    {
        public ProductEntity CreateProductEntity()
        {
            return new ProductEntity();
        }

        public StockEntity CreateStockEntity()
        {
            return new StockEntity();
        }

        public TagEntity CreateTagEntity()
        {
            return new TagEntity();
        }

        public ProductTagEntity CreateProductTagEntity()
        {
            return new ProductTagEntity();
        }
    }
}
