﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using InventoryDAL.Interfaces;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryDAL.Products;
using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks.Tests
{
    [TestClass()]
    public class StockBuilderTests
    {
        private Mock<IDomainFactory> mockDomainFactory;
        private Mock<IRepositoryFactory> mockRepositoryFactory;
        private Mock<IStockEntity> mockStockEntity;

        [TestInitialize]
        public void CreateMocks()
        {
            this.mockDomainFactory = new Mock<IDomainFactory>();
            this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
            this.mockStockEntity = new Mock<IStockEntity>().SetupAllProperties();
        }

        [TestMethod()]
        public void StockBuilder_ShouldHaveSame_Id_AsEntity_WhenCreated()
        {
            int expected = 123;
            mockStockEntity.Setup(entity => entity.Id).Returns(expected);

            StockBuilder builder = CreateStockBuilderWithMocks();
            int actual = builder.Id;

            Assert.AreEqual(expected, actual);
        }


        private StockBuilder CreateStockBuilderWithMocks()
        {
            return new StockBuilder(mockStockEntity.Object,
                                      mockDomainFactory.Object,
                                      mockRepositoryFactory.Object);
        }

        [TestMethod()]
        public void StockBuilder_ShouldHaveSame_ProductId_AsEntity_WhenCreated()
        {
            int expected = 222;
            mockStockEntity.Setup(entity => entity.ProductId).Returns(expected);

            StockBuilder builder = CreateStockBuilderWithMocks();
            int actual = builder.ProductId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void StockBuilder_ShouldHaveSame_Amount_AsEntity_WhenCreated()
        {
            int expected = 333;
            mockStockEntity.Setup(entity => entity.Amount).Returns(expected);

            StockBuilder builder = CreateStockBuilderWithMocks();
            int actual = builder.Amount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void StockBuilder_ShouldHaveSame_Date_AsEntity_WhenCreated()
        {
            DateTime expected = DateTime.Now;
            mockStockEntity.Setup(pe => pe.Date).Returns(expected);

            StockBuilder builder = CreateStockBuilderWithMocks();
            DateTime actual = builder.Date;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void StockBuilder_ShouldHaveNo_Product_WhenCreated()
        {
            Product expected = null;

            StockBuilder builder = CreateStockBuilderWithMocks();
            Product actual = builder.Product;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BuildProduct_ShouldSet_ProductId_ToMatchProductIdIn_Stock()
        {
            /* ARRANGE */
            int expectedProductId = 111;
            StockEntity basicProps = new StockEntity { Id = 111, ProductId = expectedProductId, Amount = 300, Date = DateTime.Now };
            SetBasicPropertiesInMockStockEntity(basicProps);
            SetupMockRepositoryFactoryToReturnProduct();

            /* ACT */
            StockBuilder builder = CreateStockBuilderWithMocks();
            builder.BuildProduct();

            /* ASSERT */
            int actual = builder.Product.Id;
            Assert.AreEqual(expectedProductId, actual);
        }

        private void SetupMockRepositoryFactoryToReturnProduct()
        {
            this.mockRepositoryFactory.Setup(factory => factory.ProductsRepository
                .Get(It.IsAny<int>()))
                .Returns((int id) => new Product(id, "Name", 10M, "Sku"));
        }

        [TestMethod()]
        public void GetResult_ShouldReturnStock_WithSameBasicProperties_AsStockEntity()
        {
            /* ARRANGE */
            StockEntity basicProps = new StockEntity { Id = 111, ProductId = 222, Amount = 300, Date = DateTime.Now };
            SetBasicPropertiesInMockStockEntity(basicProps);
            SetupMockDomainFactoryToReturnStock();

            /* ACT */
            StockBuilder builder = CreateStockBuilderWithMocks();
            IStock stock = builder.GetResult();

            /* ASSERT */
            Assert.AreEqual(stock.Id, basicProps.Id);
            Assert.AreEqual(stock.ProductId, basicProps.ProductId);
            Assert.AreEqual(stock.Amount, basicProps.Amount);
            Assert.AreEqual(stock.Date, basicProps.Date);
        }

        private void SetBasicPropertiesInMockStockEntity(StockEntity props)
        {
            this.mockStockEntity.Object.Id = props.Id;
            this.mockStockEntity.Object.ProductId = props.ProductId;
            this.mockStockEntity.Object.Amount = props.Amount;
            this.mockStockEntity.Object.Date = props.Date;
        }

        private void SetupMockDomainFactoryToReturnStock()
        {
            this.mockDomainFactory.Setup(factory => factory.CreateStock(It.IsAny<int>(),
                                                                        It.IsAny<int>(),
                                                                        It.IsAny<int>(),
                                                                        It.IsAny<DateTime>()
                                                                       ))
                .Returns((int id,
                          int productId,
                          int amount,
                          DateTime date
                          ) => new Stock(id, productId, amount, date));
        }


        //[TestMethod()]
        //public void GetResult_ShouldReturnStock_WithSameProduct_AsStockEntity_WhenUsedAfter_BuildProduct()
        //{
        //    /* ARRANGE */
        //    int expectedProductId = 222;
        //    StockEntity basicProps = new StockEntity { Id = 111, ProductId = expectedProductId, Amount = 300, Date = DateTime.Now };
        //    SetBasicPropertiesInMockStockEntity(basicProps);
        //    SetupMockDomainFactoryToReturnStock();
        //    SetupMockRepositoryFactoryToReturnProduct();

        //    /* ACT */
        //    StockBuilder builder = CreateStockBuilderWithMocks();
        //    builder.BuildProduct();
        //    IStock stock = builder.GetResult();

        //    /* ASSERT */
        //    int actual = stock.Product.Id;
        //    Assert.AreEqual(expectedProductId, actual);
        //}
    }
}