using System.Collections.Generic;

namespace InventoryLogic.ProductTags
{
    public interface IProductTagDAO
    {
        ProductTag Get(int id);
        List<ProductTag> GetAll();
        void Add(ProductTag tag);
        void Remove(int id);
        void Modify(ProductTag tag);
    }
}
