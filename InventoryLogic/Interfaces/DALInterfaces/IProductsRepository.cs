using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using InventoryDAL.Interfaces;

namespace InventoryDAL.Products
{
    public interface IProductsRepository : IRepository<Product>
    {
    }
}