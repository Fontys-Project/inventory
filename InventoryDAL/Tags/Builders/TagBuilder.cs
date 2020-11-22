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
        public List<Product> Products { get; set; }

        public TagBuilder(TagEntity tagEntity, IDomainFactory domainFactory, IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
            this.tagEntity = tagEntity;

            this.Id = tagEntity.Id;
            this.Name = tagEntity.Name;
            this.Products = new List<Product>();
        }

        public void BuildProducts()
        {
            List<ProductTagEntity> productTagEntities = tagEntity.ProductTagEntities;
            if (productTagEntities == null) return;
            List<Product> products = new List<Product>();
            productTagEntities.ForEach(prodTag =>
            {
                Product product = GetProduct(prodTag);
                products.Add(product);
            });
            this.Products = products;
        }

        private Product GetProduct(ProductTagEntity prodTag)
        {
            try
            {
                Product product = repositoryFactory.GetCrudRepository<Product>().Get(prodTag.ProductId);
                return product;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Product not found. Please first create the product.", e);
            }
        }

        public Tag GetResult()
        {
            return domainFactory.CreateTag(Id, Name, Products);
        }
    }
}