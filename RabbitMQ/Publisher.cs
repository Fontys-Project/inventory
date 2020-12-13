using EasyNetQ;
using InventoryLogic.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class Publisher
    {
        private readonly IBus bus;

        public Publisher(IBus bus)
        {
            this.bus = bus;
        }

        public void Publish(OrderMessage message)
        {
            bus.PubSub.Publish(message);
        }
    }
}
