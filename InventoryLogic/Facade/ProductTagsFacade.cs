using System;
using System.Collections.Generic;
using InventoryLogic.Tags;

namespace InventoryLogic.Facade
{
    public class ProductTagsFacade : IFacade<Tag>
    {
        private readonly IDatabaseFactory databaseFactory;

        public ProductTagsFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Tag Get(int id)
        {
            return databaseFactory.GetTagDAO().Get(id);
        }

        public List<Tag> GetAll()
        {
            return databaseFactory.GetTagDAO().GetAll();
        } 
        
        public Tag Add(Tag tag)
        {
            databaseFactory.GetTagDAO().Add(tag);
            return tag;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetTagDAO().Remove(id);
            return true;
        }

        public Boolean Modify(Tag tag)
        {
            databaseFactory.GetTagDAO().Modify(tag);
            return true;
        }
    }
}
