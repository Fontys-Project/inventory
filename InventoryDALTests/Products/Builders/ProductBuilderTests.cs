using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using System.Collections.Generic;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Products.Tests
{
    [TestClass()]
    public class ProductBuilderTests
    {
        private Mock<IDomainFactory> mockDomainFactory;
        private Mock<IRepositoryFactory> mockRepositoryFactory;
        private Mock<IProductEntity> mockProductEntity;

        [TestInitialize]
        public void CreateMocks()
        {
            this.mockDomainFactory = new Mock<IDomainFactory>();
            this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
            this.mockProductEntity = new Mock<IProductEntity>().SetupAllProperties();
            mockProductEntity.Setup(pe => pe.ProductTagEntities).Returns(new List<ProductTagEntity>());
            mockProductEntity.Setup(pe => pe.StockEntities).Returns(new List<StockEntity>());
        }

        [TestMethod()]
        public void Product_ShouldHaveSame_Id_AsEntity_WhenConverted()
        {
            int expected = 123;
            mockProductEntity.Setup(pe => pe.Id).Returns(expected);

            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });
            int actual = product.Id;

            Assert.AreEqual(expected, actual);
        }


        private ProductConverter CreateConverter()
        {
            return new ProductConverter(mockDomainFactory.Object,
                                      mockRepositoryFactory.Object);
        }

        [TestMethod()]
        public void Product_ShouldHaveSame_Name_AsEntity_WhenConverted()
        {
            string expected = "Name123";
            mockProductEntity.Setup(pe => pe.Name).Returns(expected);

            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });
            string actual = product.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_ShouldHaveSame_Price_AsEntity_WhenConverted()
        {
            Decimal expected = 12.50M;
            mockProductEntity.Setup(pe => pe.Price).Returns(expected);

            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });
            Decimal actual = product.Price;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_ShouldHaveSame_Sku_AsEntity_WhenConverted()
        {
            string expected = "Sku123";
            mockProductEntity.Setup(pe => pe.Sku).Returns(expected);

            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });
            string actual = product.Sku;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_ShouldHaveSame_NumberOfTags_AsEntity_WhenConverted()
        {
            int expected = mockProductEntity.Object.ProductTagEntities.Count;

            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });
            int actual = product.Tags.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_ShouldHaveSame_NumberOfStocks_AsEntity_WhenConverted()
        {
            int expected = mockProductEntity.Object.StockEntities.Count;

            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });
            int actual = product.Stocks.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_Should_Contain_Tags_With_Matching_Ids_WhenConverted()
        {
            /* ARRANGE */
            int[] expected = { 111, 222, 333 };
            SetupProductEntityToReturnProductTags(expected);
            SetupMockRepositoryFactoryToReturnTags();

            /* ACT */
            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });

            /* ASSERT */
            for (int i = 0; i < expected.Length; i++)
            {
                int actual = product.Tags[i].Id;
                Assert.AreEqual(expected[i], actual);
            }
        }

        private void SetupMockRepositoryFactoryToReturnTags()
        {
            this.mockRepositoryFactory.Setup(factory => factory.TagsRepository
                                                     .Get(It.IsAny<int>()))
                                                     .Returns((int id) => new Tag(id, "TestTag"));
        }

        private void SetupProductEntityToReturnProductTags(int[] ids)
        {
            IList<ProductTagEntity> productTagsList = new List<ProductTagEntity>();
            for (int i = 0; i < ids.Length; i++)
            {
                productTagsList.Add(new ProductTagEntity { TagId = ids[i] });
            }
            this.mockProductEntity.Setup(pe => pe.ProductTagEntities).Returns(productTagsList);
        }

        [TestMethod()]
        public void Product_Should_Contain_Stocks_With_Matching_Ids_WhenConverted()
        {
            /* ARRANGE */
            int[] expected = { 111, 222, 333 };
            SetupProductEntityToReturnStocks(expected);
            SetupMockRepositoryFactoryToReturnStocks();

            /* ACT */
            ProductConverter converter = CreateConverter();
            Product product = converter.Convert(mockProductEntity.Object, (a, b) => { });

            /* ASSERT */
            for (int i = 0; i < expected.Length; i++)
            {
                int actual = product.Stocks[i].Id;
                Assert.AreEqual(expected[i], actual);
            }
        }

        private void SetupProductEntityToReturnStocks(int[] expected)
        {
            IList<StockEntity> stockEntitiesList = new List<StockEntity>();
            for (int i = 0; i < expected.Length; i++)
            {
                stockEntitiesList.Add(new StockEntity { Id = expected[i] });
            }
            this.mockProductEntity.Setup(pe => pe.StockEntities).Returns(stockEntitiesList);
        }

        private void SetupMockRepositoryFactoryToReturnStocks()
        {
            this.mockRepositoryFactory.Setup(rf => rf.StocksRepository
                    .Get(It.IsAny<int>()))
                    .Returns((int id) => new Stock(id, 999, 888));
        }

        [TestMethod()]
        public void GetResult_ShouldReturnProduct_WithSameBasicProperties_AsProductEntity()
        {
            /* ARRANGE */
            ProductEntity expectedProps = new ProductEntity { Id = 100, Name = "Name", Price = 10.00M, Sku = "Sku" };
            SetBasicPropertiesInMockProductEntity(expectedProps);
            SetupMockDomainFactoryToReturnProduct();

            /* ACT */
            ProductConverter builder = CreateConverter();
            IProduct product = builder.Convert(expectedProps, (a, b) => { });

            /* ASSERT */
            Assert.AreEqual(product.Id, expectedProps.Id);
            Assert.AreEqual(product.Name, expectedProps.Name);
            Assert.AreEqual(product.Price, expectedProps.Price);
            Assert.AreEqual(product.Sku, expectedProps.Sku);
        }

        private void SetBasicPropertiesInMockProductEntity(ProductEntity props)
        {
            this.mockProductEntity.Object.Id = props.Id;
            this.mockProductEntity.Object.Name = props.Name;
            this.mockProductEntity.Object.Price = props.Price;
            this.mockProductEntity.Object.Sku = props.Sku;
        }

        private void SetupMockDomainFactoryToReturnProduct()
        {
            this.mockDomainFactory.Setup(df => df.CreateProduct(It.IsAny<int>(),
                                                                It.IsAny<string>(),
                                                                It.IsAny<Decimal>(),
                                                                It.IsAny<string>(),
                                                                It.IsAny<List<Tag>>(),
                                                                It.IsAny<List<Stock>>()))
                .Returns((int id, string name, Decimal price, string sku, List<Tag> tags, List<Stock> stocks)
                    => new Product(id, name, price, sku, tags, stocks));
        }

        [TestMethod()]
        public void GetResult_ShouldReturnProduct_WithSameStocks_AsProductEntity_WhenUsedAfter_BuildStocks()
        {
            /* ARRANGE */
            ProductEntity basicProps = new ProductEntity { Id = 100, Name = "Name", Price = 10.00M, Sku = "Sku" };
            int[] expectedStockIds = { 111, 222, 333 };
   
            SetBasicPropertiesInMockProductEntity(basicProps);
            SetupProductEntityToReturnStocks(expectedStockIds);
            SetupMockDomainFactoryToReturnProduct();
            SetupMockRepositoryFactoryToReturnStocks();

            /* ACT */
            ProductConverter builder = CreateConverter();
            //builder.BuildStocks();
            IProduct product = builder.Convert(mockProductEntity.Object, (a, b) => { });

            /* ASSERT */
            for (int i = 0; i < expectedStockIds.Length; i++)
            {
                int actual = product.Stocks[i].Id;
                Assert.AreEqual(expectedStockIds[i], actual);
            }
        }

        [TestMethod()]
        public void GetResult_ShouldReturnProduct_WithSameTagAssociations_AsProductEntity_WhenUsedAfter_BuildTags()
        {
            /* ARRANGE */
            ProductEntity basicProps = new ProductEntity { Id = 100, Name = "Name", Price = 10.00M, Sku = "Sku" };
            int[] expectedTagIds = { 111, 222, 333 };
            SetBasicPropertiesInMockProductEntity(basicProps);
            SetupProductEntityToReturnProductTags(expectedTagIds);
            SetupMockDomainFactoryToReturnProduct();
            SetupMockRepositoryFactoryToReturnTags();

            /* ACT */
            ProductConverter builder = CreateConverter();
            //builder.BuildTags();
            IProduct product = builder.Convert(mockProductEntity.Object, (a, b) => { });

            /* ASSERT */
            for (int i = 0; i < expectedTagIds.Length; i++)
            {
                int actual = product.Tags[i].Id;
                Assert.AreEqual(expectedTagIds[i], actual);
            }
        }
    }
}