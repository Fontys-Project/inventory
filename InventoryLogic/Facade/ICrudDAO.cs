using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public interface ICrudDAO<T>
    {
        T Get(int id);
        List<T> GetAll();
        void Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}