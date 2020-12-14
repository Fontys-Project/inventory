using InventoryLogic.EventBus;
using System;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    public class MessageHandler
    {
        private readonly IModel channel;

        public MessageHandler(IModel channel)
        {
            this.channel = channel;
        }

        public void StartListening()
        {
            var exchangeName = Environment.GetEnvironmentVariable("ORDERS_EXCHANGE");
            Subscribe(exchangeName, HandleOrder);

            exchangeName = Environment.GetEnvironmentVariable("SCANNER_EXCHANGE");
            Subscribe(exchangeName, HandleScan);
        }

        public delegate void OnMessage(string str);

        private void Subscribe(string exchange, OnMessage callback)
        {
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
            var queueName = exchange + ".inventory";
            channel.QueueDeclare(queueName); 
            Console.WriteLine(queueName);

            // Messages published to exchange, should be directed to our queue
            channel.QueueBind(queue: queueName,
                exchange: exchange,
                routingKey: "");

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [string] {0}", message);
                callback(message);
            };
            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);
        }

        private void HandleOrder(string message)
        {
            OrderMessage orderMessage = JsonConvert.DeserializeObject<OrderMessage>(message);
            Console.WriteLine("do something with OrderMessage attributes:" + orderMessage.Text);
        }

        private void HandleScan(string message)
        {
            ScannerMessage scannerMessage = JsonConvert.DeserializeObject<ScannerMessage>(message);
            Console.WriteLine("do something with ScannerMessage attributes:" + scannerMessage.Text);
        }
    }
}