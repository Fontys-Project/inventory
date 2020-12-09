using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Products;
using InventoryLogic.ProductTag;
using InventoryLogic.Stocks;
using Moq;
using InventoryLogic.Tags;

namespace InventoryLogic.Products.Tests
{
    [TestClass]
    public class ProductTest
    {

        private Mock<Stock> stockMock;
        private Mock<Tag> tagMock;

        [TestInitialize]
        public void Setup()
        {
            stockMock = new Mock<Stock>();
            tagMock = new Mock<Tag>();

        }

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

        [TestMethod("Product_Price_Returns 10.5")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetPrice()
        {
            // Arrange
            Product product = new Product(1, "testname", 10.5M, "");
            Decimal expected = 10.5M;
            // Act
            Decimal actual = product.Price;
            // Assert
            Assert.AreEqual<Decimal>(expected, actual, "Error in retrieving product price");
        }

        [TestMethod("Product_Sku_Returns fc6f9b27-0a03-475e-b5cd-9c8604ac5875")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetSku()
        {
            // Arrange
            Product product = new Product(1, "testname", 10.5M, "fc6f9b27-0a03-475e-b5cd-9c8604ac5875");
            String expected = "fc6f9b27-0a03-475e-b5cd-9c8604ac5875";
            // Act
            String actual = product.Sku;
            // Assert
            Assert.AreEqual<String>(expected, actual, "Error in retrieving product sku");
        }

        /*
        [TestMethod("Product_Stocks_Returns mockstock")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetStocks()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");
            
            Stock expected = stockMock.Object;
            stockMock.Setup(s => s.Product).Returns(product);

            // Act
            product.Stocks.Add(stockMock.Object);
            Stock actual = product.Stocks[0];
            // Assert
            Assert.AreEqual<Stock>(expected, actual, "Error in retrieving mocked stock");
        }*/


        [TestMethod("Product_Tags_Returns mocktag")]
        [TestCategory("Unit Tests")]
        [Priority(1)]
        [Owner("Richard")]
        public void GetTags()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");
            Tag expected = tagMock.Object;
            //tagMock.Setup(s => s.)
            //    .Returns(product);
            
            // Act
            product.Tags.Add(tagMock.Object);
            Tag actual = product.Tags[0];
            // Assert
            Assert.AreEqual<Tag>(expected, actual, "Error in retrieving mocked tag");
        }


    }
}
