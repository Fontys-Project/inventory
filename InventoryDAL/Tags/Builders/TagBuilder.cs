using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Tags
{
    public class TagBuilder : ITagBuilder
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly TagEntity tagEntity;

        public int Id { get; set; }
        public string Name { get; set; }

        public TagBuilder(TagEntity tagEntity, IDomainFactory domainFactory, IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
            this.tagEntity = tagEntity;

            this.Id = tagEntity.Id;
            this.Name = tagEntity.Name;
        }

        private Product GetProduct(ProductTagEntity prodTag)
        {
            Product product = repositoryFactory.GetCrudRepository<Product>().Get(prodTag.ProductId);
            if (product == null) throw new InvalidDataException("Product not found. Please first create the product.");
            return product;
        }

        public Tag GetResult()
        {
            Tag tag = domainFactory.CreateTag();
            tag.Id = this.Id;
            tag.Name = this.Name;
            return tag;
        }
    }
}
