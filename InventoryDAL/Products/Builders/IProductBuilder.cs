using InventoryLogic.Products;

namespace InventoryDAL.Products
{
    public interface IProductBuilder : IProduct
    {
        Product GetResult();
    }
}
