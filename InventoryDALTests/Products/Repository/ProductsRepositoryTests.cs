using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryDAL.Products;
using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.Products.Products;
using InventoryLogic.Interfaces;
using Moq;

namespace InventoryDAL.Products.Tests
{
    [TestClass()]
    public class ProductsRepositoryTests
    {
        private Mock<IProductEntityDAO> mockProductEntityDAO;
        private Mock<IBuilderFactory> mockBuilderFactory;

        [TestInitialize]
        public void CreateMocks()
        {
            this.mockProductEntityDAO = new Mock<IProductEntityDAO>();
            this.mockBuilderFactory = new Mock<IBuilderFactory>();
        }

        [TestMethod()]
        public void ProductsRepositoryTest()
        {
            var repo = new ProductsRepository(mockProductEntityDAO.Object, mockBuilderFactory.Object);

            Assert.IsInstanceOfType(repo, typeof(ProductsRepository));
        }

        [TestMethod()]
        public void GetAllExcludingNavigationPropertiesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetExcludingNavigationPropertiesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.Fail();
        }
    }
}