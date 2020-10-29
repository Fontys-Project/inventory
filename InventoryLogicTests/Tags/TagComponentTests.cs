using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using InventoryLogic.ProductTagJoins;

namespace InventoryLogic.Tags.Tests
{
    [TestClass()]
    public class TagComponentTests
    {
        [TestMethod]
        [Priority(10)]
        public void GetProductTagJoinsShouldReturnProductTagJoins()
        {
            var expected = new ProductTagJoin();
            Tag tag = new Tag();

            tag.ProductTagJoins.Add(expected);
            var actual = tag.ProductTagJoins[0];

            Assert.AreEqual(expected, actual);
        }
    }
}