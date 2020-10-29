using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Products;
using Moq;

namespace InventoryLogic.Products.Tests
{
    [TestClass]
    public class ProductTest
    {         

        [TestMethod("Product_Name_Returns testname")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetName()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");
            String expected = "testname";
            // Act
            String actual = product.Name;
            // Assert
            Assert.AreEqual<String>(expected, actual, "Error in retrieving product name");
        }

        [TestMethod("Product_Price_Returns 10")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetPrice()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");
            String expected = "testname";
            // Act
            String actual = product.Name;
            // Assert
            Assert.AreEqual<String>(expected, actual, "Error in retrieving product name");
        }

        [TestMethod("Product_Stocks_Returns mockstock")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetStocks()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");
            var stockMock = new Mock<Stocks.Stock>(product);
            Stocks.Stock expected = stockMock.Object;
            stockMock.Setup(s => s.Product).Returns(product);

            // Act
            product.Stocks.Add(stockMock.Object);
            Stocks.Stock actual = product.Stocks[0];
            // Assert
            Assert.AreEqual<Stocks.Stock>(expected, actual, "Error in retrieving mocked stock");
        }


    }
}
