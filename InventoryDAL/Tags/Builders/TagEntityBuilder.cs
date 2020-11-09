using InventoryDAL.Interfaces;
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
            if (products == null || products.Count == 0) return new List<ProductTagEntity>();
            List<ProductTagEntity> newProductTagEntities = new List<ProductTagEntity>();
            products.ForEach(product =>
            {
                ProductTagEntity ptEntity = GetProductTagEntity(product.Id, tagId);
                newProductTagEntities.Add(ptEntity);
            });
            return newProductTagEntities;
        }

        private ProductTagEntity GetProductTagEntity(int productId, int tagId)
        {
            ProductTagEntity ptEntity = daoFactory.ProductTagDAO.Get(productId, tagId);
            if (ptEntity == null) ptEntity = entityFactory.CreateProductTagEntity(productId, tagId, this.daoFactory);
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