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
        private readonly Tag tag;

        public int Id { get; set; }
        public string Name { get; set; }

        public TagEntityBuilder(Tag tag,
                                IEntityFactory entityFactory,
                                IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.tag = tag;

            this.Id = tag.Id;
            this.Name = tag.Name;
        }

        public TagEntity GetResult()
        {
            TagEntity tagEntity = daoFactory.TagEntityDAO.Get(this.Id);
            if (tagEntity == null) tagEntity = entityFactory.CreateTagEntity();
            tagEntity.Name = this.Name;
            return tagEntity;
        }
    }
}