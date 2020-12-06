using System.Collections.Generic;

namespace InventoryDAL.Interfaces
{
    public interface ICrudDAO<T>
    {
        T GetIncludingNavigationProperties(int id);
        // T Get(int id); // TODO: Are these used?
        List<T> GetAllIncludingNavigationProperties();
        // List<T> GetAll();
        T Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}