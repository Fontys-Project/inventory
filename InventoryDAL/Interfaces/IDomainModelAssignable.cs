using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Facade;

namespace InventoryDAL.Interfaces
{
    public interface IDomainModelAssignable<V>
    {
        public void ConvertFromDomainModel(V fromDomainModel, IDAOFactory factory);

        public void ConvertToDomainModel(V toDomainModel, IDAOFactory factory);
    }
}
