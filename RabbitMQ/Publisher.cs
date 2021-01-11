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
        }

        public void Publish(string exchange, string payload)
        {
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            // optional properties
            var props = this.channel.CreateBasicProperties();
            props.AppId = "Inventory";
            props.Persistent = true;
            // props.UserId = "ops0";
            props.MessageId = Guid.NewGuid().ToString("N");
            props.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            // body
            var body = Encoding.UTF8.GetBytes(payload);

            // publish
            channel.BasicPublish(
                exchange: exchange,
                routingKey: "", // ignored with type fanout
                basicProperties: props,
                body: body);

            Console.WriteLine(" [x] Sent: {0}", payload);
        }
    }
}
