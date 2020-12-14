using InventoryLogic.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
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
            Subscribe("ordering.inventory", HandleOrder);
            Subscribe("scanner.inventory", HandleScan);
        }

        public delegate void OnMessage(string str);

        private void Subscribe(string queueName, OnMessage callback)
        {
            channel.QueueDeclare(queueName);

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