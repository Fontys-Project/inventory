namespace InventoryDAL.Products.ProductEntities.Interfaces
{
    public interface IProductEntityBuilder : IProductEntity
    {
        ProductEntity GetResult();
    }
}