using InventoryDAL.Interfaces;

namespace InventoryDAL.ProductTag.Interfaces
{
    public interface IProductTagDAO : IHasCrudActions<ProductTagEntity>
    {
        // besides crud methods:
        public ProductTagEntity Get(int productId, int tagId);
        public void Remove(int productId, int tagId);
    }
}