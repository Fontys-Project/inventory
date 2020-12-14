using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryDAL.Products
{
    public interface IProductEntityConverter
    {
       
        ProductEntity Convert(Product product);
    }
}