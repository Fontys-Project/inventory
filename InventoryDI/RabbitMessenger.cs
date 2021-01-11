using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.EventBus;
using InventoryLogic.Facade;
using InventoryLogic.Interfaces;
using Newtonsoft.Json;
using RabbitMQ;

namespace InventoryDI
{
    public class RabbitMessenger : IEventBusPublisher
        // TODO: not at all sure about the name: Proxy, Factory, Root?
    {
        private readonly Publisher publisher;
        private readonly MessageHandler messageHandler;

        public RabbitMessenger(ProductsFacade productsFacade, StocksFacade stocksFacade)
        {
            var channel = new ConnectionService().GetChannel();
            publisher = new Publisher(channel);
            messageHandler = new MessageHandler(channel, productsFacade, stocksFacade);
            messageHandler.StartListening();
        }

        public void Publish(string exchange, string payload)
        {
            publisher.Publish(exchange, payload);
        }
    }
}
