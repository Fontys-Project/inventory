using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Products;

namespace InventoryLogic.Products.Tests
{
    [TestClass]
    public class ProductTest
    {

        [TestMethod("")]
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


    }
}
