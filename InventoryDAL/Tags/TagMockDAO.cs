﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryLogic.Tags;
using System;

namespace InventoryDAL.ProductTags
{
    public class TagMockDAO : DbContext, ITagDAO
    {
        private readonly List<Tag> tags;

       public TagMockDAO()
        {
            tags = new List<Tag>
            {
                new Tag(1, "Clothes"),
                new Tag(2, "Fruit"),
                new Tag(3, "Meat")
            };
        }

        public void Add(Tag tag)
        {
            this.tags.Add(tag);
        }

        public List<Tag> GetAll()
        {
            return this.tags;
        }

        public Tag Get(int id)
        {
            foreach (Tag tag in this.tags)
            {
                if (tag.Id == id)
                    return tag;
            }
            return null;
        }

        public void Modify(Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}