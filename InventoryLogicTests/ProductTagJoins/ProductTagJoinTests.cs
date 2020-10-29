using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;

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

        [TestMethod()]
        public void TagTest1()
        {
            Assert.Fail();
        }
    }
}