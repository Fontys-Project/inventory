using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.EventBus;
using Newtonsoft.Json;
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
            var channel = new ConnectionService().GetChannel();
            publisher = new Publisher(channel);
            messageHandler = new MessageHandler(channel);
            messageHandler.StartListening();
        }

        public void Publish(string exchange, string routingKey, string payload)
        {
            publisher.Publish(exchange, routingKey, payload);
        }
    }
}
