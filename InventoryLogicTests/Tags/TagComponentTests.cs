using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using InventoryLogic.ProductTag;
using InventoryLogic.Products;

namespace InventoryLogic.Tags.Tests
{
    [TestClass()]
    public class TagComponentTests
    {
        [TestMethod]
        [Priority(10)]
        public void GetProductTagJoinsShouldReturnProductTagJoins()
        {
            var expected = new Product();
            Tag tag = new Tag();

            tag.Products.Add(expected);
            var actual = tag.Products[0];

            Assert.AreEqual(expected, actual);
        }
    }
}