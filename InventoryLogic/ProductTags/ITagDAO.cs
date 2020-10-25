using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public interface ITagDAO
    {
        Tag Get(int id);
        List<Tag> GetAll();
        void Add(Tag tag);
        void Remove(int id);
        void Modify(Tag tag);
    }
}
