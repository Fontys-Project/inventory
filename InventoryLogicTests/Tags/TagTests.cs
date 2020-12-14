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
    public class TagTests
    {
        //[TestMethod()]
        //public void CreateTag()
        //{
        //    Tag tag = new Tag();
        //}

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
        public void GetNameShouldReturnNull()
        {
            Tag tag = new Tag(999, null);
            string expected = null;

            string actual = tag.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNameShouldReturnSetName()
        {
            Tag tag = new Tag(1, "name");
            string expected = "name";

            string actual = tag.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetIdShouldReturnSetId()
        {
            Tag tag = new Tag(245,"");
            int expected = 245;

            int actual = tag.Id;

            Assert.AreEqual(expected, actual);
        }
/*
        [TestMethod]
        public void GetProductTagJoinsShouldReturnProductTagJoins()
        {
            var mock = new Mock<Product>();
            mock.Setup(j => j.Id).Returns(1);
            var mockProduct = mock.Object;
            Tag tag = new Tag();

            tag.Products.Add(mockProduct);
            var actual = tag.Products[0];

            Assert.AreEqual(mockProduct, actual);
        }*/
    }
}