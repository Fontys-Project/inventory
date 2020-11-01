using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Facade
{
    public interface IHasUniqueObjectId
    {
        public int Id { get;  }
    }
}
