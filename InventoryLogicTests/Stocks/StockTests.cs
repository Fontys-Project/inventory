using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using InventoryLogic.Products;
using InventoryLogic.ProductTagJoins;
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
        private List<ProductTagJoin> tagList;
        private List<Stock> stockList;

        [TestInitialize]
        public void Setup()
        {
            productMock = new Mock<Product>();

            // arrange
            //
            productMock.Setup(p => p.Id).Returns(10);
            productMock.Setup(p => p.Name).Returns("Mondkapje");
            productMock.Setup(p => p.Price).Returns(5);
            productMock.Setup(p => p.Sku).Returns("mondkapjeSKU");

            var productTagJoinMock = new Mock<ProductTagJoin>();
            var stocklistMock = new Mock<Stock>();

            tagList = new List<ProductTagJoin>()
            {
                productTagJoinMock.Object
            };

            stockList = new List<Stock>()
            {
                stocklistMock.Object
            };
        }

        [TestMethod]

        public void GetID()
        {
            // Arrange
            var mock = new Mock<Product>();
            mock.Setup(p => p.Id).Returns(1);
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