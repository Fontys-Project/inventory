using InventoryDAL.Products.PropertyProducts.Interfaces;

namespace InventoryDAL.Products.PropertyProducts
{
    public class PropertyProductFactory : IPropertyProductFactory
    {
        public PropertyProduct Create(int id, string name, decimal price, string sku)
        {
            return new PropertyProduct(id, name, price, sku);
        }
    }
}
