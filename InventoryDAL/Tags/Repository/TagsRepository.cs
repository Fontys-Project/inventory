using InventoryDAL.Interfaces;
using InventoryLogic.Tags;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Tags
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IBuilderFactory builderFactory; 
        private readonly ITagEntityDAO tagEntityDAO;

        public TagsRepository(ITagEntityDAO tagEntityDAO, IBuilderFactory builderFactory)
        {
            this.builderFactory = builderFactory; 
            this.tagEntityDAO = tagEntityDAO;
        }

        public List<Tag> GetAllExcludingNavigationProperties()
        {
            List<TagEntity> tagEntities = tagEntityDAO.GetAllIncludingNavigationProperties();
            return tagEntities
                .Select(tagEntity => BuildTag(tagEntity, false))
                .ToList();
        }

        public List<Tag> GetAll()
        {
            List<TagEntity> tagEntities = tagEntityDAO.GetAllIncludingNavigationProperties();
            return tagEntities
                .Select(tagEntity => BuildTag(tagEntity, true))
                .ToList();
        }

        public Tag GetExcludingNavigationProperties(int id)
        {
            TagEntity tagEntity = tagEntityDAO.GetIncludingNavigationProperties(id);
            return BuildTag(tagEntity, false);
        }

        public Tag Get(int id)
        {
            TagEntity tagEntity = tagEntityDAO.GetIncludingNavigationProperties(id);
            return BuildTag(tagEntity, true);
        }

        public Tag Add(Tag tag)
        {
            TagEntity tagEntity = BuildTagEntity(tag, false);
            tagEntity = tagEntityDAO.Add(tagEntity);
            return BuildTag(tagEntity, true);
        }

        public void Modify(Tag tag)
        {
            TagEntity tagEntity = BuildTagEntity(tag, false);
            tagEntityDAO.Modify(tagEntity);
        }

        public void Remove(int id)
        {
            tagEntityDAO.Remove(id);
        }

        private Tag BuildTag(TagEntity tagEntity, bool includesNavigationProperties)
        {
            var tagBuilder = builderFactory.CreateTagBuilder(tagEntity);
            if (includesNavigationProperties)
            {
                tagBuilder.BuildProducts();
            }
            return tagBuilder.GetResult();
        }

        private TagEntity BuildTagEntity(Tag tag, bool includesNavigationProperties)
        {
            var tagEntityBuilder = builderFactory.CreateTagEntityBuilder(tag);
            if (includesNavigationProperties)
            {
                tagEntityBuilder.BuildProductTagEntities();
            }
            return tagEntityBuilder.GetResult();
        }
    }
}