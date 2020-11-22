using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using InventoryLogic.Products;
using InventoryLogic.ProductTag;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;


namespace InventoryLogic.Stocks.Tests
{
    [TestClass()]
    public class StockTests
    {
        [TestInitialize]
        public void SetUp()
        {
           
        }

        [TestMethod("Stock returns Id ")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Mark")]
        public void GetId()
        {
            // Arrange
            Product product = new Product(1, "Mondkapje", 1, "MondkapjeTest");
            Stock stock = new Stock(1, product, 10);
            int expected = 1;
            // Act
            int actual = stock.Id;
            // Assert
            Assert.AreEqual<int>(expected, actual, "Id not correct");
        }

        [TestMethod("Stock returns amount ")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Mark")]
        public void GetAmount()
        {
            // Arrange
            Product product = new Product(1, "Mondkapje", 1, "MondkapjeTest");
            Stock stock = new Stock(1, product, 10);
            int expected = 10;
            // Act
            int actual = stock.Amount;
            // Assert
            Assert.AreEqual<int>(expected, actual, "Id not correct");
        }
    }
        
}