using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class ConnectionFactory
    {
        private readonly IBus bus;

        public ConnectionFactory()
        {
            var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");
            bus = RabbitHutch.CreateBus($"host={rabbitHostName ?? "localhost"}"); 
        }

        public IBus GetBus()
        { 
            return bus;
        }
    }
}
