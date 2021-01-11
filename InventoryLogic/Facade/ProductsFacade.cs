using System;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using InventoryLogic.Tags;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : CrudDTOFacade<Product,ProductDTO>
    {
        public ProductsFacade(IRepositoryFactory repoFactory)
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
            repoFactory.GetCrudRepository<Product>().Modify(product);
            return true;
        }

        public bool RemoveTag(int productId, int tagId)
        {
            Product product = repoFactory.GetCrudRepository<Product>().Get(productId);
            Tag tag = repoFactory.GetCrudRepository<Tag>().Get(tagId);

            if (product == null) throw new ArgumentException("Product not found.");
            if (tag == null) throw new ArgumentException("Tag not found.");
            if (!product.Tags.Contains(tag)) return false;

            product.Tags.Remove(tag);
            repoFactory.GetCrudRepository<Product>().Modify(product);
            return true;
        }
    }
}
