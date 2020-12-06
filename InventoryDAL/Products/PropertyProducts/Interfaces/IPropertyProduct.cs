namespace InventoryDAL.Products.PropertyProducts.Interfaces
{
    public interface IPropertyProduct
    {
        int Id { get; }
        string Name { get; }
        decimal Price { get; }
        string Sku { get; }
    }
}