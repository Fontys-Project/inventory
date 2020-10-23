using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;

namespace InventoryLogic.Facade
{
    public interface IFacade<Type>
    {
        public Type Get(int id);
        public List<Type> GetAll();
        public Type Add(Type o);
        public Boolean Remove(int id);
        public Boolean Modify(Type obj, int id);
    }
}
