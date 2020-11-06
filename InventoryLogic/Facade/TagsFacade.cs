using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Tags;

namespace InventoryLogic.Facade
{
    public class TagsFacade : CrudFacade<Tag>
    {
        public TagsFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }

        public bool ApplyTag(int productId, int tagId)
        {
            Product product = repoFactory.GetCrudRepository<Product>().Get(productId);
            Tag tag = repoFactory.GetCrudRepository<Tag>().Get(tagId);
            if (!product.Tags.Contains(tag))
                product.Tags.Add(tag);

            repoFactory.GetCrudRepository<Product>().Modify(product);
           
            return true;
        }
    }
}
