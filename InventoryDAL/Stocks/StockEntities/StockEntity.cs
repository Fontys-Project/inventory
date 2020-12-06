using System;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Stocks.StockEntities.Interfaces;

namespace InventoryDAL.Stocks.StockEntities
{
    public class StockEntity : IStockEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Constructor used by .net API framwork
        public StockEntity()
        {

        }
    }
}
