using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;

namespace InventoryLogic.Facade
{
    public class TagsFacade : CrudDTOFacade<Tag, TagDTO>
    {
        public TagsFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }

        public bool ApplyTag(int productId, int tagId)
        {
            Product product = repoFactory.GetCrudRepository<Product>().Get(productId);
            Tag tag = repoFactory.GetCrudRepository<Tag>().Get(tagId);

            if (product == null) throw new ArgumentException("Product not found.");
            if (tag == null) throw new ArgumentException("Tag not found.");
            if (product.Tags.Contains(tag)) return false;

            product.Tags.Add(tag);
            tag.Products.Add(product);

            repoFactory.GetCrudRepository<Product>().Modify(product);
            return true;
        }
    }
}
