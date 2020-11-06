using InventoryLogic.Interfaces;
using InventoryLogic.Tags;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Tags
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ITagEntityDAO tagEntityDAO;
        private readonly ITagConverter tagConverter;

        public TagsRepository(ITagEntityDAO tagEntityDAO, ITagConverter tagConverter)
        {
            this.tagEntityDAO = tagEntityDAO;
            this.tagConverter = tagConverter;
        }

        public void Add(Tag tag)
        {
            tagEntityDAO.Add(tagConverter.ConvertToTagEntity(tag));
        }

        public List<Tag> GetAll()
        {
            return tagEntityDAO.GetAll()
                .Select(entity => tagConverter.ConvertToTag(entity)).ToList();
        }

        public Tag Get(int id)
        {
            TagEntity entity = tagEntityDAO.Get(id);
            return tagConverter.ConvertToTag(entity);
        }

        public void Modify(Tag tag)
        {
            TagEntity entity = tagConverter.ConvertToTagEntity(tag);
            tagEntityDAO.Modify(entity);
        }

        public void Remove(int id)
        {
            tagEntityDAO.Remove(id);
        }
    }
}
