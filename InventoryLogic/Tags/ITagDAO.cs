using InventoryLogic.Facade;
using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public interface ITagDAO : ICrudDAO<Tag>
    {
        Tag Get(int id);
        List<Tag> GetAll();
        void Add(Tag tag);
        void Remove(int id);
        void Modify(Tag tag);
    }
}
