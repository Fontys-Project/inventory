using System;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public class CrudFacade<T> : ICrudFacade<T>
    {
        protected readonly IDatabaseFactory databaseFactory;

        public CrudFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public List<T> GetAll()
        {
            return databaseFactory.GetCrudDAO<T>().GetAll();
        }

        public T Get(int id)
        {
            return databaseFactory.GetCrudDAO<T>().Get(id);
        }
        
        public T Add(T obj)
        {
            databaseFactory.GetCrudDAO<T>().Add(obj);
            return obj;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetCrudDAO<T>().Remove(id);
            return true;
        }

        public Boolean Modify(T obj)
        {
            databaseFactory.GetCrudDAO<T>().Modify(obj);
            return true;
        }
    }
}
