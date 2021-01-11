using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.EventBus
{
    public interface IEventBusPublisher
    {
        public void Publish(string exchange, string payload); 
    }
}
