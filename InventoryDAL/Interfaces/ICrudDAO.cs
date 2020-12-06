using System.Collections.Generic;

namespace InventoryDAL.Interfaces
{
    public interface ICrudDAO<T>
    {
        T Get(int id);
        // T Get(int id); // TODO: Are these used?
        List<T> GetAll();
        // List<T> GetAll();
        T Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}