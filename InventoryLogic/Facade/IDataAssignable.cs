using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace InventoryLogic.Facade
{
    public interface IDataAssignable<V>
    {
        public void TransferDataFromView(V fromView);
        public void TransferDataToView(V toView);
    }
}
