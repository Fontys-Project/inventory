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
            bus = RabbitHutch.CreateBus("host=my-rabbit;port=5672"); // TODO: put in config file or environment variables?
        }

        public IBus GetBus()
        { 
            return bus;
        }
    }
}
