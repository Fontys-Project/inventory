using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Product;
using InventoryLogic.ProductTags;

namespace InventoryLogic.Facade
{
    public class ProductTagsFacade
    {

        private readonly IDatabaseFactory databaseFactory;

        public ProductTagsFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public ProductTag Get(int id)
        {
            return databaseFactory.GetProductTagDAO().Get(id);
        }

        public List<ProductTag> Get()
        {
            return databaseFactory.GetProductTagDAO().GetAll();
        } 
        
        public ProductTag Add(int id, string name)
        {
            ProductTag tag = new ProductTag(id, name); //TODO: instantiëren??
            databaseFactory.GetProductTagDAO().Add(tag);
            return tag;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetProductTagDAO().Remove(id);
            return true;
        }

        public Boolean Modify(ProductTag tag)
        {
            databaseFactory.GetProductTagDAO().Modify(tag);
            return true;
        }
    }
}
