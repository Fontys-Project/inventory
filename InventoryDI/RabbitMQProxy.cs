using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.EventBus;
using RabbitMQ;

namespace InventoryDI
{
    public class RabbitMQProxy : IEventBusMessenger
        // TODO: not at all sure about the name Proxy or Factory. Looks a lot like a composition 'root' to me.
    {
        private RabbitMQMessenger rabbitMQMessenger;

        public RabbitMQProxy()
        {
            rabbitMQMessenger = new RabbitMQMessenger(new ConnectionFactory().GetBus());
        }

        public void Publish(Message message)
        {
            rabbitMQMessenger.Publish(message);
        }

        public void Subscribe()
        {
            rabbitMQMessenger.Subscribe();
        }
    }
}
