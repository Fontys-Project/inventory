using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Facade;

namespace InventoryDAL.Database
{
    public interface IDomainModelAssignable<V>
    {
        public void ConvertFromDomainModel(V fromDomainModel, IDatabaseFactory factory);

        public void ConvertToDomainModel(V toDomainModel, IDatabaseFactory factory);
    }
}
