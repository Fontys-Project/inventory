using System.Collections.Generic;

namespace InventoryLogic.ProductTag
{
    public interface IProductTagDAO
    {
        public List<ProductTag> GetAll();
        public ProductTag Get(int productId, int tagId);
        public void Add(ProductTag obj);
        public void Modify(ProductTag obj);
        public void Remove(int productId, int tagId);
    }
}