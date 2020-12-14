using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Interfaces;

namespace InventoryDAL.Interfaces
{
    public interface IRepository<T> : ICrudRepository<T>
    {
       // T GetExcludingNavigationProperties(int id);
       // List<T> GetAllExcludingNavigationProperties();
    }
}
