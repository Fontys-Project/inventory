﻿using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Tags
{
    public class TagEntityBuilder : ITagEntityBuilder
    {
        private readonly IDAOFactory daoFactory;
        private readonly IEntityFactory entityFactory;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductTagEntity> ProductTagEntities { get; set; }

        public TagEntityBuilder(Tag tag,
                                IEntityFactory entityFactory,
                                IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;

            this.Id = tag.Id;
            this.Name = tag.Name;
            this.ProductTagEntities = GetProductTagEntities(tag.Id, tag.Products);
        }

        private List<ProductTagEntity> GetProductTagEntities(int tagId, List<Product> products)
        {
            if (products == null || products.Count == 0) return null;
            List<ProductTagEntity> newProductTagEntities = new List<ProductTagEntity>();
            products.ForEach(product =>
            {
                ProductTagEntity ptEntity = GetProductTagEntity(tagId, product);
                newProductTagEntities.Add(ptEntity);
            });
            return newProductTagEntities;
        }

        private ProductTagEntity GetProductTagEntity(int tagId, Product product)
        {
            ProductTagEntity ptEntity = daoFactory.ProductTagDAO.Get(product.Id, tagId);
            if (ptEntity == null) throw new InvalidDataException("" +
                "Product-Tag relationship not found. " +
                "Please apply the tag using the dedicated method.");
            return ptEntity;
        }

        public TagEntity Build()
        {
            TagEntity tagEntity = daoFactory.TagEntityDAO.Get(this.Id);
            if (tagEntity == null) tagEntity = entityFactory.CreateTagEntity();
            tagEntity.Name = this.Name;
            tagEntity.ProductTagEntities = this.ProductTagEntities;
            return tagEntity;
        }
    }
}