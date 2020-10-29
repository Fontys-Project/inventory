using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using InventoryLogic.Products;

namespace InventoryLogic.ProductTagJoins.Tests
{
    [TestClass()]
    public class ProductTagJoinTests
    {
        [TestMethod()]
        public void CreateProductTagJoin()
        {
            ProductTagJoin join = new ProductTagJoin();
        }

        [TestMethod]
        public void GetProductIdShouldReturnProductId()
        {
            ProductTagJoin join = new ProductTagJoin();
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
            ProductTagJoin join = new ProductTagJoin();

            join.Product = expected;
            var actual = join.Product;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagIdShouldReturnTagId()
        {
            ProductTagJoin join = new ProductTagJoin();
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
            ProductTagJoin join = new ProductTagJoin();

            join.Tag = expected;
            Tag actual = join.Tag;

            Assert.AreEqual(expected, actual);
        }
    }
}