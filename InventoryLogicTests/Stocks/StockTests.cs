using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Stocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Stocks.Tests
{
    [TestClass()]
    public class StockTests
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        [TestMethod()]
        public void GetID()
        {
            // Arrange
            Stock stock = new Stock(1,1,(1,)
            int expected = 1;
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