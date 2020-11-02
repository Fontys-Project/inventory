using System.Collections.Generic;

namespace InventoryLogic.ProductTagJoins
{
    public interface IProductTagJoinDAO
    {
        public List<ProductTagJoin> GetAll();
        public ProductTagJoin Get(int productId, int tagId);
        public void Add(ProductTagJoin obj);
        public void Modify(ProductTagJoin obj);
        public void Remove(int productId, int tagId);
    }
}