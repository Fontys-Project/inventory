﻿using InventoryDAL.Interfaces;

namespace InventoryDAL.ProductTag
{
    public interface IProductTagDAO : ICrudDAO<ProductTagEntity>
    {
        // besides crud methods:
        public ProductTagEntity GetIncludingNavigationProperties(int productId, int tagId);
        public void Remove(int productId, int tagId);
    }
}