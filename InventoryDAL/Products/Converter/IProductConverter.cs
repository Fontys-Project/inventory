using InventoryLogic.Products;

namespace InventoryDAL.Products
{
    public interface IProductConverter
    {
        Product ConvertToProduct(ProductEntity e);
        ProductEntity ConvertToExistingProductEntity(Product product);
        ProductEntity ConvertToNewProductEntity(Product product);
    }
}