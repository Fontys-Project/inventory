using InventoryLogic.Products;

namespace InventoryDAL.Products
{
    public interface IProductConverter
    {
        Product ConvertToProduct(ProductEntity e);
        ProductEntity ConvertToProductEntity(Product product);
    }
}