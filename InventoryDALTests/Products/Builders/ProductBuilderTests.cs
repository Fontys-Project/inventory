using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;

namespace InventoryDAL.Products.Tests
{
    [TestClass()]
    public class ProductBuilderTests
    {
        private Mock<IDomainFactory> mockDomainFactory;
        private Mock<IRepositoryFactory> mockRepositoryFactory;
        private Mock<IProductEntity> mockEntity;

        [TestInitialize]
        public void SetupMocks()
        {
            this.mockDomainFactory = new Mock<IDomainFactory>();
            this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
            this.mockEntity = new Mock<IProductEntity>();
        }

        private ProductBuilder CreateProductBuilder()
        {
            return new ProductBuilder(mockEntity.Object,
                                      mockDomainFactory.Object,
                                      mockRepositoryFactory.Object);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Id_AsEntity_WhenCreated()
        {
            int expected = 123;
            mockEntity.Setup(pe => pe.Id).Returns(expected);

            ProductBuilder builder = CreateProductBuilder();
            int actual = builder.Id;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Name_AsEntity_WhenCreated()
        {
            string expected = "Name123";
            mockEntity.Setup(pe => pe.Name).Returns(expected);

            ProductBuilder builder = CreateProductBuilder();
            string actual = builder.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Price_AsEntity_WhenCreated()
        {
            Decimal expected = 12.50M;
            mockEntity.Setup(pe => pe.Price).Returns(expected);

            ProductBuilder builder = CreateProductBuilder();
            Decimal actual = builder.Price;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Sku_AsEntity_WhenCreated()
        {
            string expected = "Sku123";
            mockEntity.Setup(pe => pe.Sku).Returns(expected);

            ProductBuilder builder = CreateProductBuilder();
            string actual = builder.Sku;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSameEmpty_Tags_List_WhenCreated()
        {
            int expected = 0;

            ProductBuilder builder = CreateProductBuilder();
            int actual = builder.Tags.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSameEmpty_Stocks_List_WhenCreated()
        {
            int expected = 0;

            ProductBuilder builder = CreateProductBuilder();
            int actual = builder.Stocks.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BuildTags_ShouldSet_TagIds_ToMatchIdsIn_ProductTags()
        {
            int[] expected = { 111, 222, 333 };
            for (int i = 0; i < expected.Length; i++)
            {
                this.mockEntity.Setup(pe => pe.ProductTagEntities[i]).Returns(
                    new ProductTagEntity { TagId = expected[i] });
            }

            ProductBuilder builder = CreateProductBuilder();
            builder.BuildTags();
            for (int i = 0; i < expected.Length; i++)
            {
                int actual = builder.Tags[i].Id;
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod("BuildTags_ShouldSet_ProductIdsInTags_ToMatch_ProductId")]
        public void BuildTags_ShouldSet_ProductIdsInTags_ToMatch_ProductId()
        {
            int expected = 123;
            int[] tagIds = { 111, 222, 333 };
            this.mockEntity.Setup(pe => pe.Id).Returns(expected);
            for (int i = 0; i < tagIds.Length; i++)
            {
                this.mockEntity.Setup(pe => pe.ProductTagEntities[i]).Returns(
                    new ProductTagEntity { TagId = tagIds[i] });
            }

            ProductBuilder builder = CreateProductBuilder();
            builder.BuildTags();
            for (int i = 0; i < tagIds.Length; i++)
            {
                int actual = builder.Tags[i].Id;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void BuildStocks_ShouldSet_StockIds_ToMatchIdsIn_StockEntities()
        {
            int[] expected = { 100, 200, 300 };
            for (int i = 0; i < expected.Length; i++)
            {
                this.mockEntity.Setup(pe => pe.StockEntities[i]).Returns(
                    new StockEntity{ Id = expected[i] });
            }

            ProductBuilder builder = CreateProductBuilder();
            builder.BuildStocks();
            for (int i = 0; i < expected.Length; i++)
            {
                int actual = builder.Stocks[i].Id;
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod("BuildStocks_ShouldSet_ProductIdsInStocks_ToMatch_ProductId")]
        public void BuildStocks_ShouldSet_ProductIdsInStocks_ToMatch_ProductId()
        {
            int expected = 123;
            int[] stockIds = { 111, 222, 333 };
            this.mockEntity.Setup(pe => pe.Id).Returns(expected);
            for (int i = 0; i < stockIds.Length; i++)
            {
                this.mockEntity.Setup(pe => pe.StockEntities[i]).Returns(
                    new StockEntity { Id = stockIds[i] });
            }

            ProductBuilder builder = CreateProductBuilder();
            builder.BuildStocks();
            for (int i = 0; i < stockIds.Length; i++)
            {
                int actual = builder.Stocks[i].Id;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetResult_ShouldReturnProduct()
        {
            ProductBuilder builder = CreateProductBuilder();
            IProduct product = builder.GetResult();
        }
    }
}