using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IDomainFactory
    {
        Tag CreateTag();
        Stock CreateStock();
        Product CreateProduct();
        ProductDTO CreateProductDTO();
    }
}