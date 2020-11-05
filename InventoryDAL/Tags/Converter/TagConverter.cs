using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;

namespace InventoryDAL.Tags
{
    public class TagConverter : ITagConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IConverterFactory converterFactory;

        public TagConverter(IDomainFactory domainFactory, IDAOFactory daoFactory, IConverterFactory converterFactory)
        {
            this.domainFactory = domainFactory;
            this.daoFactory = daoFactory;
            this.converterFactory = converterFactory;
        }

        public Tag ConvertToTag(TagEntity e)
        {
            Tag tag = domainFactory.CreateTag();
            tag.Id = e.Id;
            tag.Name = e.Name;
            e.ProductTagEntities.ForEach(j =>
            {
                ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(j.ProductId);
                Product product = converterFactory.ProductConverter.ConvertToProduct(productEntity);
                tag.Products.Add(product);
            });
            return tag;
        }

        public TagEntity ConvertToTagEntity(Tag tag)
        {
            TagEntity tagEntity = daoFactory.TagEntityDAO.Get(tag.Id);
            tagEntity.Id = tag.Id;
            tagEntity.Name = tag.Name;
            tagEntity.ProductTagEntities.Clear();

            tag.Products.ForEach(product =>
            {
                ProductTagEntity prodTag = daoFactory.ProductTagEntityDAO.Get(product.Id, tag.Id);
                tagEntity.ProductTagEntities.Add(prodTag);
            });

            return tagEntity;
        }
    }
}
