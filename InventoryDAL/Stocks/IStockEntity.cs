using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Products;

namespace InventoryDAL.Stocks
{
    public interface IStockEntity
    {
        int Id { get; set; }
        int ProductId { get; set; }
        ProductEntity ProductEntity { get; set; }
        int Amount { get; set; }
        DateTime Date { get; set; }
    }
}
