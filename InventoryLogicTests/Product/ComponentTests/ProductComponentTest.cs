using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Products;
using InventoryLogic.Stocks;

namespace InventoryLogic.Products.Tests
{
    [TestClass]
    public class ProductComponentTest
    {
        [TestMethod("Product_Stocks_Returns Stock")]
        [TestCategory("Component Tests")]
        [Priority(10)]
        [Owner("Richard")]
        public void GetStocks()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");
            Stock stock = new Stock(1,product,10);
            Stock expected = stock;
            // Act
            product.Stocks.Add(stock);
            Stock actual = product.Stocks[0];
            // Assert
            Assert.AreEqual<Stock>(expected, actual, "Error in retrieving mocked stock");
        }


    }

}