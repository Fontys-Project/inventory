using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.EventBus
{
    public interface IEventBusMessenger
    {
        public void Publish(Message message);
        public void Subscribe();
    }
}
