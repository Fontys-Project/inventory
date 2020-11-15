using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;

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
            if (product == null) throw new ArgumentException("Product not found.");
            Tag tag = repoFactory.GetCrudRepository<Tag>().Get(tagId);
            if (product == null) throw new ArgumentException("Tag not found.");

            if (product.Tags.Contains(tag)) return false;

            product.Tags.Add(tag);
            repoFactory.GetCrudRepository<Product>().Modify(product);
            return true;
        }
    }
}
