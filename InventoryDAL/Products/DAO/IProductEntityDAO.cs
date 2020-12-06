using InventoryDAL.Interfaces;
using System.Collections.Generic;

namespace InventoryDAL.Products
{
    public interface IProductEntityDAO : ICrudDAO<ProductEntity>
    {
        List<ProductEntity> GetAllWith(int tagId);
    }
}