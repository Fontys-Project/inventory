using EasyNetQ;
using InventoryLogic.EventBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Tests
{
    [TestClass()]
    public class RabbitMQMessengerTests
    {    
        [TestMethod()]
        public void RabbitMQMessengerTest()
        {
            IBus bus = new ConnectionFactory().GetBus(); 
            var messenger = new Publisher();
        }

        [DataTestMethod]
        [DataRow("test")]
        [DataRow("order")]
        [DataRow("lala")]
        public void PublishTest(string input)
        {
            // quick integration testing, refactor!
            IBus bus = new ConnectionFactory().GetBus();
            Publisher m = new Publisher(bus);
            m.Publish(new OrderMessage{
                Text = input
            });
        }

        [TestMethod()]
        public void SubscribeTest()
        {
            Assert.Fail();
        }
    }
}