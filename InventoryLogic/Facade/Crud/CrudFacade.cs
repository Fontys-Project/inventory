using InventoryLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public class CrudFacade<T> : ICrudFacade<T>
    {
        protected readonly IRepositoryFactory repoFactory;

        public CrudFacade(IRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        public List<T> GetAll()
        {
            return repoFactory.GetCrudRepository<T>().GetAll();
        }

        public T Get(int id)
        {
            return repoFactory.GetCrudRepository<T>().Get(id);
        }
        
        public T Add(T obj)
        {
            repoFactory.GetCrudRepository<T>().Add(obj);
            return obj;
        }

        public Boolean Remove(int id)
        {
            repoFactory.GetCrudRepository<T>().Remove(id);
            return true;
        }

        public Boolean Modify(T obj)
        {
            repoFactory.GetCrudRepository<T>().Modify(obj);
            return true;
        }
    }
}
