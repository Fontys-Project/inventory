namespace InventoryDAL.Products.PropertyProducts.Interfaces
{
    public interface IPropertyProductFactory
    {
        PropertyProduct Create(int id, string name, decimal price, string sku);
    }
}