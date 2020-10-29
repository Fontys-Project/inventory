using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Stocks;
using InventoryLogic.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;


namespace InventoryLogic.Stocks.Tests
{
    [TestClass()]
    public class StockTests
    {

        private Stock stock;
        private Mock<Product> productMock;
        private Mock<Stock> stockMock;

        [SetUp]
        public void Setup()
        {
            productMock = new Mock<Product>();
            stockMock = new Mock<Stock>();

            // arrange
            //
            productMock.Setup(id => Product.Id).Returns(10);
            productMock.Setup( => Product.Id).Returns(10);
            productMock.Setup(id => Product.Id).Returns(10);
            productMock.Setup(id => Product.Id).Returns(10);
        }
        
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        [TestMethod()]
        public void GetID()
        {
            // Arrange
            var mock = new Mock<Product>();
            mock.Setup(p => p.Id = 1).Returns(1);
            Product expected = mock.Object;

            Stock stock = new Stock(1, expected, 30);
            
            // Act
            int actual = stock.Id;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void StockTest1()
        {
            Assert.Fail();
        }
    }
}