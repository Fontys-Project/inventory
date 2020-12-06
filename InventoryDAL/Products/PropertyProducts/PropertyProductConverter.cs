using InventoryDAL.Products.PropertyProducts;
using InventoryDAL.Products.PropertyProducts.Interfaces;
using InventoryDAL.Products.Converters.Interfaces;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.ProductEntities.Interfaces;

namespace InventoryDAL.Products
{
    public class PropertyProductConverter : IPropertyProductConverter
    {
        private readonly IPropertyProductFactory propertyProductFactory;

        public PropertyProductConverter(IPropertyProductFactory propertyProductFactory)
        {
            this.propertyProductFactory = propertyProductFactory;
        }

        public PropertyProduct Convert(IProductEntity productEntity)
        {
            
            return propertyProductFactory.Create(productEntity.Id,
                                                            productEntity.Name,
                                                            productEntity.Price,
                                                            productEntity.Sku);
        }
    }
}
