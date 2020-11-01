using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Facade
{
    public class CrudViewFacade<T, V> : ICrudFacade<V> where T : IDataAssignable<V>, new() where V : IHasUniqueObjectId,new()
    {
        protected readonly IDatabaseFactory databaseFactory;

        public CrudViewFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public List<V> GetAll()
        {
            List<V> newViews = new List<V>();
            List<T> records = databaseFactory.GetCrudDAO<T>().GetAll();

            foreach(T record in records)
            {
                V newView = new V();
                record.TransferDataToView(newView);
                newViews.Add(newView);
            }

            return newViews;
        }

        public V Get(int id)
        {
            V newView = new V();
            T obj = databaseFactory.GetCrudDAO<T>().Get(id);
            obj.TransferDataToView(newView);

            return newView;
        }

        public V Add(V obj)
        {
            T newObj = new T();

            newObj.TransferDataFromView(obj);
            
            databaseFactory.GetCrudDAO<T>().Add(newObj);
            newObj.TransferDataToView(obj);

            return obj;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetCrudDAO<T>().Remove(id);
            return true;
        }

        public Boolean Modify(V obj)
        {
            T objToEdit = databaseFactory.GetCrudDAO<T>().Get(obj.Id);
            objToEdit.TransferDataFromView(obj);

            databaseFactory.GetCrudDAO<T>().Modify(objToEdit);
            return true;
        }
    }
}
