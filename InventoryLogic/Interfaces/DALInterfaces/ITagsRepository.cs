using InventoryLogic.Interfaces;
using InventoryLogic.Tags;
using System.Collections.Generic;
using InventoryDAL.Interfaces;

namespace InventoryDAL.Tags
{
    public interface ITagsRepository : IRepository<Tag>
    {
        void RemoveFromCache(Tag tag);
    }
}