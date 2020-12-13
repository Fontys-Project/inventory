using EasyNetQ;
using InventoryLogic.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class MessageHandler
    {
        private readonly IBus bus;

        public MessageHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void StartListening()
        {
            bus.PubSub.Subscribe<OrderMessage>("orders", HandleOrder);
            bus.PubSub.Subscribe<ScannerMessage>("scans", HandleScan);
        }

        private void HandleOrder(OrderMessage orderMessage)
        {
            Console.WriteLine("do something with OrderMessage attributes:" + orderMessage.Text);
        }

        private void HandleScan(ScannerMessage scannerMessage)
        {
            Console.WriteLine("do something with ScannerMessage attributes:" + scannerMessage.Text);
        }
    }
}