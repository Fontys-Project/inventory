using System;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public interface ICrudFacade<T>
    {
        public List<T> GetAll();
        public T Get(int id);
        public T Add(T obj);
        public Boolean Remove(int id);
        public Boolean Modify(T obj);
    }
}