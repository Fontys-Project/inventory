using System.Collections.Generic;
using InventoryLogic.Products;

namespace InventoryLogic.Interfaces
{
    public interface ICrudRepository<T>
    {
        // used by facade

        T Get(int id);
        List<T> GetAll();
        T Add(T obj);
        void Remove(int id);
        void Modify(T obj);
    }
}