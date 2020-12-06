using System.Collections.Generic;
using System.Linq;
using InventoryDAL.Factories.Interfaces;
using InventoryLogic.Tags;

namespace InventoryDAL.Tags.Repository
{
    public class PropertyTagSupplier : IPropertyTagSupplier
    {
        private readonly IBuilderFactory builderFactory; 
        private readonly ITagEntityDAO tagEntityDAO;

        public PropertyTagSupplier(ITagEntityDAO tagEntityDAO, IBuilderFactory builderFactory)
        {
            this.builderFactory = builderFactory; 
            this.tagEntityDAO = tagEntityDAO;
        }

        public List<Tag> GetAllExcludingNavigationProperties()
        {
            List<TagEntity> tagEntities = tagEntityDAO.GetAll();
            return tagEntities
                .Select(BuildTag)
                .ToList();
        }

        public Tag Get(int id)
        {
            TagEntity tagEntity = tagEntityDAO.Get(id);
            return BuildTag(tagEntity);
        }

        private Tag BuildTag(TagEntity tagEntity)
        {
            var tagBuilder = builderFactory.CreateTagBuilder(tagEntity);
            return tagBuilder.GetResult();
        }
    }
}