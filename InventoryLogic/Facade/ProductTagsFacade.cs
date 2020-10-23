using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;
using InventoryLogic.ProductTags;

namespace InventoryLogic.Facade
{
    public class ProductTagsFacade : IFacade<ProductTag>
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

        public List<ProductTag> GetAll()
        {
            return databaseFactory.GetProductTagDAO().GetAll();
        } 
        
        public ProductTag Add(ProductTag tag)
        {
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
