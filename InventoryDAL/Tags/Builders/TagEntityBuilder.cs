using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;
using System.IO;

namespace InventoryDAL.Tags
{
    public class TagConverter : ITagConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IEntityFactory entityFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IBuilderFactory converterFactory;

        public TagConverter(IDomainFactory domainFactory, IEntityFactory entityFactory, IDAOFactory daoFactory, IBuilderFactory converterFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.converterFactory = converterFactory;
        }

        public Tag ConvertToTag(TagEntity e)
        {
            Tag tag = domainFactory.CreateTag();
            tag.Id = e.Id;
            tag.Name = e.Name;
            if (e.ProductTagEntities != null)
            {
                e.ProductTagEntities.ForEach(j =>
                {
                    ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(j.ProductId);
                    Product product = converterFactory.ProductBuilder.Build(productEntity);
                    tag.Products.Add(product);
                });
            }
            return tag;
        }

        public TagEntity ConvertToNewTagEntity(Tag tag)
        {
            TagEntity tagEntity = entityFactory.CreateTagEntity();
            return Map(tag, tagEntity);
        }

        public TagEntity ConvertToExistingTagEntity(Tag tag)
        {
            TagEntity tagEntity = daoFactory.TagEntityDAO.Get(tag.Id);
            if (tagEntity == null) throw new InvalidDataException("TagEntity by that id not found");
            return Map(tag, tagEntity);
        }

        private TagEntity Map(Tag tag, TagEntity tagEntity)
        {
            if (!(tag.Products == null || tag.Products.Count == 0))
                throw new InvalidDataException("Cannot change products this way");

            tagEntity.Id = tag.Id;
            tagEntity.Name = tag.Name;

            return tagEntity;
        }
    }
}
