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
        private readonly Dictionary<Tag, ITagEntity> tagCache;

        public TagsRepository(ITagEntityDAO tagEntityDAO, IConverterFactory converterFactory)
        {
            this.converterFactory = converterFactory;
            this.tagEntityDAO = tagEntityDAO;
            tagCache = new Dictionary<Tag, ITagEntity>();
        }

        // Handle cacheing of object on instantiation
        private void OnObjectCreation(Tag tag, ITagEntity tagEntity)
        {
            RemoveFromCache(tag);
            tagCache.Add(tag, tagEntity);
        }

        public List<Tag> GetAll()
        {
            List<TagEntity> tagEntities = tagEntityDAO.GetAll();

            // Trigger with where, only products not cached, and then select all uncached product entities to convert Products that will be added
            // to the cache with the OnObjectCreation delegate.
            tagEntities.Where(tagEntity => tagCache.Values.Any(cacheEntity => cacheEntity.Id == tagEntity.Id) == false)
                .ToList().ForEach(tagEntity => converterFactory.tagConverter.Convert(tagEntity, OnObjectCreation));

            return tagCache.Keys.ToList<Tag>();
        }

        public Tag Get(int id)
        {
            Tag tag = tagCache.Keys.Where(t => t.Id == id).FirstOrDefault();
            if (tag == null)
            {
                TagEntity tagEntity = tagEntityDAO.Get(id);
                tag = converterFactory.tagConverter.Convert(tagEntity, OnObjectCreation);
            }
            return tag;
        }

        public Tag Add(Tag tag)
        {
            TagEntity tagEntity = converterFactory.tagEntityConverter.Convert(tag);
            tagEntity = tagEntityDAO.Add(tagEntity);
            return converterFactory.tagConverter.Convert(tagEntity, OnObjectCreation);
        }

        public void Modify(Tag tag)
        {
            RemoveFromCache(tag);
            TagEntity tagEntity = converterFactory.tagEntityConverter.Convert(tag);
            tagEntityDAO.Modify(tagEntity);
        }

        public void RemoveFromCache(Tag tag) // used in Facade. TODO: solve in DAL
        {
            Tag tagInCache = tagCache.Keys.Where(t => t.Id == tag.Id).FirstOrDefault();
            if (tagInCache != null) tagCache.Remove(tagInCache);
        }

        public void Remove(int id)
        {
            Tag tagInCache = tagCache.Keys.Where(t => t.Id == id).FirstOrDefault();
            if (tagInCache != null) tagCache.Remove(tagInCache);
            tagEntityDAO.Remove(id);
        }

        public Tag CreateNew()
        {
            return new Tag(-1, "");
        }
    }
}