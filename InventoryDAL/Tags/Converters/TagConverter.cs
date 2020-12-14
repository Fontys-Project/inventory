using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using static InventoryDAL.Tags.ITagConverter;

namespace InventoryDAL.Tags
{
    public class TagConverter : ITagConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;
        

        public TagConverter(IDomainFactory domainFactory, IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
           
        }

        public Tag Convert(ITagEntity tagEntity, OnObjectCreation onObjectCreation)
        {
            //compose Tag
            List<Product> products = new List<Product>();

            Tag tag = domainFactory.CreateTag(tagEntity.Id
                , tagEntity.Name
                , products);

            // handle instantiated tags. Needed by repository to prevent looping.
            onObjectCreation(tag, tagEntity);

            // gather child objects: products
            foreach (ProductTagEntity productTagEntity in tagEntity.ProductTagEntities)
            {
                Product product = repositoryFactory.ProductsRepository.Get(productTagEntity.ProductId);
                products.Add(product);
            }
            return tag;
        }
    }
}