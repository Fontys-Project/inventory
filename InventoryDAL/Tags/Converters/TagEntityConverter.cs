using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Tags
{
    public class TagEntityConverter : ITagEntityConverter
    {
        private readonly IDAOFactory daoFactory;
        private readonly IEntityFactory entityFactory;
      

        public TagEntityConverter(IEntityFactory entityFactory,
                                IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;

        }

        private ProductTagEntity GetProductTagEntity(int productId, int tagId)
        {
            ProductTagEntity ptEntity = daoFactory.ProductTagDAO.Get(productId, tagId);
            if (ptEntity == null) ptEntity = entityFactory.CreateProductTagEntity(productId, tagId, this.daoFactory);
            return ptEntity;
        }

        public TagEntity Convert(Tag tag)
        {
            TagEntity tagEntity = daoFactory.TagEntityDAO.Get(tag.Id);
            if (tagEntity == null) tagEntity = entityFactory.CreateTagEntity();
            tagEntity.Name = tag.Name;
            tag.Products.ForEach(product =>
            {
                ProductTagEntity productTagEntity = GetProductTagEntity(product.Id, tag.Id);
                if(!tagEntity.ProductTagEntities.Contains(productTagEntity)) 
                    tagEntity.ProductTagEntities.Add(productTagEntity);
            });
            
            return tagEntity;
        }
    }
}