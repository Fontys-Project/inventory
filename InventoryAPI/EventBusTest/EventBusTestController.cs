using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryLogic.EventBus;

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
            publisher.Publish(new OrderMessage
            {
                Text = text
            });
        }
    }
}
