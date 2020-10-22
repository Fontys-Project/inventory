using System.Collections.Generic;

namespace InventoryLogic.ProductTags
{
    public interface IProductTagDAO
    {
        ProductTag GetTag(int id);
        List<ProductTag> GetAllTags();
        void AddTag(ProductTag tag);
        void RemoveTag(int id);
        void ModifyTag(ProductTag tag);
    }
}
