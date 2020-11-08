using InventoryDAL.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Factories
{
    public class DomainFactory : IDomainFactory
    {
        public Product CreateProduct()
        {
            return new Product();
        }

        public Stock CreateStock()
        {
            return new Stock();
        }

        public Tag CreateTag()
        {
            return new Tag();
        }

        public ProductDTO CreateProductDTO()
        {
            return new ProductDTO();
        }
    }
}
