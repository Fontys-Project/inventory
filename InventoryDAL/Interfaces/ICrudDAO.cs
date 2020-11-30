using System.Collections.Generic;

namespace InventoryDAL.Interfaces
{
    public interface ICrudDAO<T>
    {
        // used by factory

        T Get(int id);
        List<T> GetAll();
        List<T> GetAllWithNavigationProperties();
        T Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}