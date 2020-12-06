using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryLogic.Products;

namespace InventoryDAL.Products.Products.Interfaces
{
    public interface IProductConverter
    {
        Product Convert(IProductEntity productEntity);
    }
}
