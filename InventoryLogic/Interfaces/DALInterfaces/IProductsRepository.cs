using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryDAL.Products
{
    public interface IProductsRepository : ICrudRepository<Product>
    {
    }
}