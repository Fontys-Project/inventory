using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using System.Collections.Generic;

namespace InventoryDAL.Products
{
    public interface IProductEntityBuilder : IProductEntity
    {
        ProductEntity Build();
    }
}