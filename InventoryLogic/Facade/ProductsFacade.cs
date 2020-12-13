using InventoryLogic.EventBus;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : CrudDTOFacade<Product, ProductDTO>
    {
        public ProductsFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }
    }
}
