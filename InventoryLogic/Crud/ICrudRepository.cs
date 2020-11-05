using System.Collections.Generic;

namespace InventoryLogic.Crud
{
    public interface ICrudRepository<T>
    {
        // used by factory

        T Get(int id);
        List<T> GetAll();
        void Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}