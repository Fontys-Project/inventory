using EasyNetQ;
using InventoryLogic.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class RabbitMQMessenger : IEventBusMessenger
    {
        private readonly IBus bus;

        public RabbitMQMessenger(IBus bus)
        {
            this.bus = bus;
        }

        public void Publish(Message message)
        {
            bus.PubSub.Publish(message);
        }

        public void Subscribe()
        {
            bus.PubSub.Subscribe<Message>("orders", HandleTextMessage);
        }

        private void HandleTextMessage(Message message)
        {
            Console.WriteLine("do something with message attributes");
        }
    }
}
