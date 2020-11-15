using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace InventoryLogic.Interfaces
{
    public interface IDataAssignable<V>
    {
        public void ConvertFromDTO(V fromDTO);
        public void ConvertToDTO(V toDTO);
    }
}
