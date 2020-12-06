using InventoryDAL.Products.PropertyProducts;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.ProductEntities.Interfaces;

namespace InventoryDAL.Products.Converters.Interfaces
{
    public interface IPropertyProductConverter
    {
        PropertyProduct Convert(IProductEntity productEntity);
    }
}