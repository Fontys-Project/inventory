using System.Collections.Generic;
using System;

namespace InventoryDAL.Tags
{
    public class TagEntityMockDAO : ITagEntityDAO
    {
        private readonly List<TagEntity> tagEntities;

       public TagEntityMockDAO()
        {
            tagEntities = new List<TagEntity>
            {
                new TagEntity{ Id = 1, Name = "Clothes" },
                new TagEntity{ Id = 2, Name = "Fruit" },
                new TagEntity{ Id = 3, Name = "Meat" }
            };
        }

        public TagEntity Add(TagEntity entity)
        {
            this.tagEntities.Add(entity);
            return entity;
        }

        public List<TagEntity> GetAllExcludingNavigationProperties()
        {
            return this.tagEntities;
        }

        public List<TagEntity> GetAll()
        {
            return this.tagEntities;
        }

        public TagEntity Get(int id)
        {
            foreach (TagEntity entity in this.tagEntities)
            {
                if (entity.Id == id)
                    return entity;
            }
            return null;
        }

        public TagEntity GetExcludingNavigationProperties(int id)
        {
            foreach (TagEntity entity in this.tagEntities)
            {
                if (entity.Id == id)
                    return entity;
            }
            return null;
        }

        public void Modify(TagEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
