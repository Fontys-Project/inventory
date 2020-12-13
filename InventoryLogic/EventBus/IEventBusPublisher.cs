using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.EventBus
{
    public interface IEventBusPublisher
    {
        // TODO: Does Inventory need to publish at all? Or just listen for messages?
        public void Publish(OrderMessage message); 
    }
}
