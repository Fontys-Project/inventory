using InventoryLogic.EventBus;
using System;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using InventoryLogic.Facade;
using System.Collections.Generic;
using InventoryLogic.Products;
using InventoryLogic.Stocks;

namespace RabbitMQ
{
    public class MessageHandler
    {
        private readonly IModel channel;
        private readonly ProductsFacade productsFacade;
        private readonly StocksFacade stocksFacade;

        public MessageHandler(IModel channel, ProductsFacade productsFacade, StocksFacade stocksFacade)
        {
            this.channel = channel;
            this.productsFacade = productsFacade;
            this.stocksFacade = stocksFacade;
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
            Publisher publisher = new Publisher(channel); // to send response back.
            List<ProductDTO> products = productsFacade.GetAll();
            ProductDTO product = products.Find(p => p.Sku.Equals(scannerMessage.Barcode, StringComparison.InvariantCultureIgnoreCase));

            if(product != null)
            {
                scannerMessage.ProductName = product.Name;
                scannerMessage.ProductPrice = product.Price;
                int totalStock = 0;
                foreach(StockDTO s in stocksFacade.GetAll().FindAll(s => s.ProductId == product.Id))
                {
                    totalStock += s.Amount;
                }
                scannerMessage.ProductStock = totalStock;
                // is there a stock with the same date?
                StockDTO stock = stocksFacade.GetAll().Find(s => s.ProductId == product.Id && s.Date.Year == DateTime.Now.Year && s.Date.Month == DateTime.Now.Month
                && s.Date.Day == DateTime.Now.Day);
                if(stock != null)
                {
                    stock.Amount += 1; // add 1
                    //product.Stocks.Find(s => s.Id == stock.Id).Amount += 1;
                    product.Stocks.Clear();
                    stocksFacade.Modify(stock);
                    productsFacade.Modify(product);
                    scannerMessage.ProductStock += 1;
                    stock.Date = DateTime.Now; // adjust time to now to reflect change.
                    if (true)
                    {
                        // send message back
                        scannerMessage.ScannerResult = ScannerResult.AddedToStock; // indicate success
                        publisher.Publish(Environment.GetEnvironmentVariable("SCANNER_RESPONSE_EXCHANGE"),
                            JsonConvert.SerializeObject(scannerMessage));
                    } else
                    {
                        // send message back
                        scannerMessage.ScannerResult = ScannerResult.UnknownError; // indicate unknown error
                        publisher.Publish(Environment.GetEnvironmentVariable("SCANNER_RESPONSE_EXCHANGE"),
                            JsonConvert.SerializeObject(scannerMessage));
                    }
                } else 
                {
                    // create new stock
                    StockDTO stockDTO = new StockDTO();
                    stockDTO.Amount = 1;
                    scannerMessage.ProductStock += 1;
                    stockDTO.Date = DateTime.Now;
                    stockDTO.Product = product;
                    stockDTO.ProductId = product.Id;
                    stockDTO = stocksFacade.Add(stockDTO);
                    product.Stocks.Add(stockDTO);
                    productsFacade.Modify(product); // needed to update product in memory cache with stock.

                    if (stockDTO.Id > 0)
                    {
                        // send message back
                        scannerMessage.ScannerResult = ScannerResult.AddedToStock; // indicate success
                        publisher.Publish(Environment.GetEnvironmentVariable("SCANNER_RESPONSE_EXCHANGE"),
                            JsonConvert.SerializeObject(scannerMessage));
                    }
                    else
                    {
                        // send message back
                        scannerMessage.ScannerResult = ScannerResult.UnknownError; // indicate unknown error
                        publisher.Publish(Environment.GetEnvironmentVariable("SCANNER_RESPONSE_EXCHANGE"),
                            JsonConvert.SerializeObject(scannerMessage));
                    }
                }



            } else
            { // product not found, send message back
                scannerMessage.ScannerResult = ScannerResult.UnknownSku; // indicate unknown error
                publisher.Publish(Environment.GetEnvironmentVariable("SCANNER_RESPONSE_EXCHANGE"),
                    JsonConvert.SerializeObject(scannerMessage));
            }

            //Console.WriteLine("do something with ScannerMessage attributes:" + scannerMessage.Barcode);
        }
    }
}