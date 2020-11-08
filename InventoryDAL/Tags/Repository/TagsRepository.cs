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

        public List<Tag> GetAll()
        {
            List<TagEntity> tagEntities = tagEntityDAO.GetAll();
            return tagEntities
                .Select(tagEntity => BuildTag(tagEntity))
                .ToList();
        }

        public Tag Get(int id)
        {
            TagEntity tagEntity = tagEntityDAO.Get(id);
            return BuildTag(tagEntity);
        }

        public void Add(Tag tag)
        {
            TagEntity tagEntity = BuildTagEntity(tag);
            tagEntityDAO.Add(tagEntity);
        }

        public void Modify(Tag tag)
        {
            TagEntity tagEntity = BuildTagEntity(tag);
            tagEntityDAO.Modify(tagEntity);
        }

        public void Remove(int id)
        {
            tagEntityDAO.Remove(id);
        }

        private Tag BuildTag(TagEntity tagEntity)
        {
            var tagBuilder = builderFactory.CreateTagBuilder(tagEntity);
            return tagBuilder.Build();
        }

        private TagEntity BuildTagEntity(Tag tag)
        {
            var tagEntityBuilder = builderFactory.CreateTagEntityBuilder(tag);
            return tagEntityBuilder.Build();
        }
    }
}