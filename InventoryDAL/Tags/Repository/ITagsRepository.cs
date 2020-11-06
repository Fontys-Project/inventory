using InventoryLogic.Interfaces;
using InventoryLogic.Tags;
using System.Collections.Generic;

namespace InventoryDAL.Tags
{
    public interface ITagsRepository : ICrudRepository<Tag>
    {
    }
}