using System.Collections.Generic;

namespace InventoryDAL.Interfaces
{
    public interface IHasCrudActions<T>
    {
        T Get(int id);
        List<T> GetAll();
        T Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}