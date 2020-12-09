using InventoryDAL.Interfaces;
using InventoryLogic.Tags;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Tags
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IConverterFactory converterFactory; 
        private readonly ITagEntityDAO tagEntityDAO;
        private readonly Dictionary<Tag,ITagEntity> tagCache;

        public TagsRepository(ITagEntityDAO tagEntityDAO, IConverterFactory converterFactory)
        {
            this.converterFactory = converterFactory; 
            this.tagEntityDAO = tagEntityDAO;
            tagCache = new Dictionary<Tag, ITagEntity>();
        }

        // Handle cacheing of object on instantiation
        private void OnObjectCreation(Tag tag, ITagEntity tagEntity)
        {
            tagCache.Add(tag, tagEntity);
        }

        public List<Tag> GetAll()
        {
            List<TagEntity> tagEntities = tagEntityDAO.GetAll();

            // Trigger with where, only products not cached, and then select all uncached product entities to convert Products that will be added
            // to the cache with the OnObjectCreation delegate.
            tagEntities.Where(tagEntity => tagCache.Values.Any(cacheEntity => cacheEntity.Id == tagEntity.Id) == false)
                .Select(tagEntity => converterFactory.tagConverter.Convert(tagEntity, OnObjectCreation));

            return tagCache.Keys.ToList<Tag>();
        }

        public Tag Get(int id)
        {
            Tag tag = tagCache.Keys.Where(t => t.Id == id).FirstOrDefault();
            if (tag == null)
            {
                TagEntity tagEntity = tagEntityDAO.Get(id);
                return converterFactory.tagConverter.Convert(tagEntity, OnObjectCreation);
            }
            else
            {
                return tag;
            }

        }

        public Tag Add(Tag tag)
        {
            TagEntity tagEntity = converterFactory.tagEntityConverter.Convert(tag);
            tagEntity = tagEntityDAO.Add(tagEntity);
            return converterFactory.tagConverter.Convert(tagEntity, OnObjectCreation);
        }

        public void Modify(Tag tag)
        {
            TagEntity tagEntity = converterFactory.tagEntityConverter.Convert(tag);
            tagEntityDAO.Modify(tagEntity);
        }

        public void Remove(int id)
        {
            tagEntityDAO.Remove(id);
        }

    }
}