using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Tags.Tests
{
    [TestClass()]
    public class TagTests
    {
        [TestMethod()]
        public void CreateTag()
        {
            Tag tag = new Tag();
        }

        [TestMethod()]
        public void ConstructTagWithParameterName()
        {
            string expected = "TestTag";

            Tag tag = new Tag(1, expected);
            string actual = tag.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConstructTagWithParameterId()
        {
            int expected = 1;

            Tag tag = new Tag(expected, "TestTag");
            int actual = tag.Id;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetNameNullShouldGetNull()
        {
            Tag tag = new Tag();
            string expected = "name";

            tag.Name = expected;
            string actual = tag.Name;

            Assert.AreEqual(expected, actual);
        }


    }
}