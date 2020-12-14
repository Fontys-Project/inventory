using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryLogic.EventBus;
using Newtonsoft.Json;

namespace InventoryAPI.EventBusTest
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class EventBusTestController : Controller
    {
        private readonly IEventBusPublisher publisher;

        public EventBusTestController(IEventBusPublisher publisher)
        {
            this.publisher = publisher;
        }

        /// <summary>
        /// Publish a message to the event bus for testing purposes
        /// </summary>
        [HttpGet]
        public void Publish(string text = "lalala")
        {
            // TODO: Does Inventory need to publish at all? Or just listen for messages?
            
            // optional: use model to create a message
            var message = new OrderMessage
            {
                Text = text
            };

            // But in the end you will need a string
            string payload = JsonConvert.SerializeObject(message);

            // Example for Order microservice
            var exchangeName= Environment.GetEnvironmentVariable("ORDERS_EXCHANGE");
            publisher.Publish(exchangeName, payload);
        }
    }
}
