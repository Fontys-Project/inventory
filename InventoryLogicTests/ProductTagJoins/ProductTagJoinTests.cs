using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using InventoryLogic.Products;

namespace InventoryLogic.ProductTag.Tests
{
    [TestClass()]
    public class ProductTagJoinTests
    {
        [TestMethod()]
        public void CreateProductTagJoin()
        {
            ProductTag join = new ProductTag();
        }

        [TestMethod]
        public void GetProductIdShouldReturnProductId()
        {
            ProductTag join = new ProductTag();
            int expected = 245;

            join.ProductId = expected;
            int actual = join.ProductId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetProductShouldReturnProduct()
        {
            var mock = new Mock<Product>();
            Product expected = mock.Object;
            ProductTag join = new ProductTag();

            join.Product = expected;
            var actual = join.Product;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagIdShouldReturnTagId()
        {
            ProductTag join = new ProductTag();
            int expected = 245;

            join.TagId = expected;
            int actual = join.TagId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagShouldReturnTag()
        {
            var mock = new Mock<Tag>();
            Tag expected = mock.Object;
            ProductTag join = new ProductTag();

            join.Tag = expected;
            Tag actual = join.Tag;

            Assert.AreEqual(expected, actual);
        }
    }
}