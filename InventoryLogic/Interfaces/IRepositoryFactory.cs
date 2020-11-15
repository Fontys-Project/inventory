using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Interfaces
{
    public interface IRepositoryFactory
    {
        ICrudRepository<T> GetCrudRepository<T>();
    }
}
