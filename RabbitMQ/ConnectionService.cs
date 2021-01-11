using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class ConnectionService
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public ConnectionService()
        {
            var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");

            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitHostName ?? "localhost",
                Port = 5672
                // UserName = "ops0",
                // Password = "ops0"
            };
            this.connection = connectionFactory.CreateConnection();
            this.channel = connection.CreateModel();
        }

        public IModel GetChannel()
        { 
            return channel;
        }
    }
}
