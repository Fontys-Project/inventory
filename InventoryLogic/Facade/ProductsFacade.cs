using InventoryLogic.Products;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : CrudFacade<Product>
    {
        public ProductsFacade(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
