using InventoryLogic.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class Publisher
    {
        private readonly IModel channel;

        public Publisher(IModel channel)
        {
            this.channel = channel;
            this.channel.ConfirmSelect();
        }

        public void Publish(string exchange, string routingKey, string payload)
        {
            var props = this.channel.CreateBasicProperties();
            props.AppId = "Inventory";
            props.Persistent = true;
            // props.UserId = "ops0";
            props.MessageId = Guid.NewGuid().ToString("N");
            props.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            var body = Encoding.UTF8.GetBytes(payload);
            this.channel.BasicPublish(exchange, routingKey, props, body);
            this.channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
        }
    }
}
