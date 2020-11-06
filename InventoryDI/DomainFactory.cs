using InventoryDAL.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDI
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
    }
}
