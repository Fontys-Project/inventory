using InventoryLogic.Products;

namespace InventoryDAL.Products
{
    public interface IProductConverter
    {
        public delegate void OnObjectCreation(Product product, IProductEntity productEntity);

        public Product Convert(IProductEntity productEntity, OnObjectCreation onObjectCreation);
    }
}
