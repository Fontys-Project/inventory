using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : CrudViewFacade<Product,ProductDTO>
    {
        public ProductsFacade(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }
}
