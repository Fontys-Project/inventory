using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Facade
{
    public class CrudViewFacade<DomainModel, DataTransferObject> : ICrudFacade<DataTransferObject> where DomainModel : IDataAssignable<DataTransferObject>, new() where DataTransferObject : IHasUniqueObjectId,new()
    {
        protected readonly IDatabaseFactory databaseFactory;

        public CrudViewFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public List<DataTransferObject> GetAll()
        {
            List<DataTransferObject> newViews = new List<DataTransferObject>();
            List<DomainModel> records = databaseFactory.GetCrudDAO<DomainModel>().GetAll();

            foreach(DomainModel record in records)
            {
                DataTransferObject newView = new DataTransferObject();
                record.ConvertToDTO(newView);
                newViews.Add(newView);
            }

            return newViews;
        }

        public DataTransferObject Get(int id)
        {
            DataTransferObject newView = new DataTransferObject();
            DomainModel obj = databaseFactory.GetCrudDAO<DomainModel>().Get(id);
            obj.ConvertToDTO(newView);

            return newView;
        }

        public DataTransferObject Add(DataTransferObject obj)
        {
            DomainModel newObj = new DomainModel();

            newObj.ConvertFromDTO(obj);
            
            databaseFactory.GetCrudDAO<DomainModel>().Add(newObj);
            newObj.ConvertToDTO(obj);

            return obj;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetCrudDAO<DomainModel>().Remove(id);
            return true;
        }

        public Boolean Modify(DataTransferObject obj)
        {
            DomainModel objToEdit = databaseFactory.GetCrudDAO<DomainModel>().Get(obj.Id);
            objToEdit.ConvertFromDTO(obj);

            databaseFactory.GetCrudDAO<DomainModel>().Modify(objToEdit);
            return true;
        }
    }
}
