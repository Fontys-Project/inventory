using InventoryDAL.Interfaces;

namespace InventoryDAL.ProductTag
{
    public interface IProductTagDAO : ICrudDAO<ProductTagEntity>
    {
        // besides crud methods:
        public ProductTagEntity Get(int productId, int tagId);
        public void Remove(int productId, int tagId);
    }
}