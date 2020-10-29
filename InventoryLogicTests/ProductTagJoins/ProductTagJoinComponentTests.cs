using InventoryLogic.Products;
using InventoryLogic.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InventoryLogic.ProductTagJoins.Tests
{
    [TestClass()]
    public class ProductTagJoinComponentTests
    {
        [TestMethod]
        [Priority(10)]
        public void GetProductShouldReturnProduct()
        {
            Product expected = new Product();
            ProductTagJoin join = new ProductTagJoin();

            join.Product = expected;
            Product actual = join.Product;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [Priority(10)]
        public void GetTagShouldReturnTag()
        {
            Tag expected = new Tag();
            ProductTagJoin join = new ProductTagJoin();

            join.Tag = expected;
            Tag actual = join.Tag;

            Assert.AreEqual(expected, actual);
        }
    }
}