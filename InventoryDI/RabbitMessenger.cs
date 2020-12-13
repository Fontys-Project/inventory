using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.EventBus;
using RabbitMQ;

namespace InventoryDI
{
    public class RabbitMessenger : IEventBusPublisher
        // TODO: not at all sure about the name: Proxy, Factory, Root?
    {
        private readonly Publisher publisher;
        private readonly MessageHandler messageHandler;

        public RabbitMessenger()
        {
            var bus = new ConnectionFactory().GetBus();
            publisher = new Publisher(bus);
            messageHandler = new MessageHandler(bus);
            messageHandler.StartListening();
        }

        public void Publish(OrderMessage message)
        {
            publisher.Publish(message);
        }
    }
}
