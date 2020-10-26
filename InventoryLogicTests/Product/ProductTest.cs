using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Products;

namespace InventoryTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetName()
        {
            // Arrange
            Product product = new Product(1, "testname", 1, "");

            // Act
            String name = product.Name;

            // Assert

            Assert.AreEqual<String>("testname", name, "Error in retrieving product name");

        }
    }
}
