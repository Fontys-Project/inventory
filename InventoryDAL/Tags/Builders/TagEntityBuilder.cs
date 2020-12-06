using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System.Collections.Generic;
using System.IO;
using InventoryDAL.Factories.Interfaces;

namespace InventoryDAL.Tags
{
    public class TagEntityBuilder : ITagEntityBuilder
    {
        private readonly IDAOFactory daoFactory;
        private readonly IEntityFactory entityFactory;
        private readonly Tag tag;


        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ProductTagEntity> ProductTagEntities { get; set; }

        public TagEntityBuilder(Tag tag,
                                IEntityFactory entityFactory,
                                IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.tag = tag;

            this.Id = tag.Id;
            this.Name = tag.Name;
            this.ProductTagEntities = new List<ProductTagEntity>();
        }

        public void BuildProductTagEntities()
        {
            List<Product> products = tag.Products;
            if (products == null || products.Count == 0) return;
            List<ProductTagEntity> newProductTagEntities = new List<ProductTagEntity>();
            products.ForEach(product =>
            {
                ProductTagEntity ptEntity = GetProductTagEntity(product.Id, tag.Id);
                newProductTagEntities.Add(ptEntity);
            });
            this.ProductTagEntities = newProductTagEntities;
        }

        private ProductTagEntity GetProductTagEntity(int productId, int tagId)
        {
            ProductTagEntity ptEntity = daoFactory.ProductTagDAO.Get(productId, tagId);
            if (ptEntity == null) ptEntity = entityFactory.CreateProductTagEntity(productId, tagId, this.daoFactory);
            return ptEntity;
        }

        public TagEntity GetResult()
        {
            TagEntity tagEntity = daoFactory.TagEntityDAO.Get(this.Id);
            if (tagEntity == null) tagEntity = entityFactory.CreateTagEntity();
            tagEntity.Name = this.Name;
            tagEntity.ProductTagEntities = this.ProductTagEntities;
            return tagEntity;
        }
    }
}