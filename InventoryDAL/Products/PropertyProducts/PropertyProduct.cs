using InventoryDAL.Products.PropertyProducts.Interfaces;

namespace InventoryDAL.Products.PropertyProducts
{
    public class PropertyProduct : IPropertyProduct
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Sku { get; }

        public PropertyProduct(int id, string name, decimal price, string sku)
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
        }
    }
}
