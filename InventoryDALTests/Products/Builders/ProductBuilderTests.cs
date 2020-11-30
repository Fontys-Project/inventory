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
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Id_AsEntity_WhenCreated()
        {
            int expected = 123;
            mockProductEntity.Setup(pe => pe.Id).Returns(expected);

            ProductBuilder builder = CreateProductBuilderWithMocks();
            int actual = builder.Id;

            Assert.AreEqual(expected, actual);
        }

        private ProductBuilder CreateProductBuilderWithMocks()
        {
            return new ProductBuilder(mockProductEntity.Object,
                                      mockDomainFactory.Object,
                                      mockRepositoryFactory.Object);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Name_AsEntity_WhenCreated()
        {
            string expected = "Name123";
            mockProductEntity.Setup(pe => pe.Name).Returns(expected);

            ProductBuilder builder = CreateProductBuilderWithMocks();
            string actual = builder.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Price_AsEntity_WhenCreated()
        {
            Decimal expected = 12.50M;
            mockProductEntity.Setup(pe => pe.Price).Returns(expected);

            ProductBuilder builder = CreateProductBuilderWithMocks();
            Decimal actual = builder.Price;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSame_Sku_AsEntity_WhenCreated()
        {
            string expected = "Sku123";
            mockProductEntity.Setup(pe => pe.Sku).Returns(expected);

            ProductBuilder builder = CreateProductBuilderWithMocks();
            string actual = builder.Sku;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveEmpty_Tags_List_WhenCreated()
        {
            int expected = 0;

            ProductBuilder builder = CreateProductBuilderWithMocks();
            int actual = builder.Tags.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveEmpty_Stocks_List_WhenCreated()
        {
            int expected = 0;

            ProductBuilder builder = CreateProductBuilderWithMocks();
            int actual = builder.Stocks.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BuildTags_ShouldSet_TagIds_ToMatchIdsIn_ProductTags()
        {
            /* ARRANGE */
            int[] expected = { 111, 222, 333 };
            SetProductTagsInMockProductEntity(expected);
            SetupMockRepositoryFactoryToReturnTags();

            /* ACT */
            ProductBuilder builder = CreateProductBuilderWithMocks();
            builder.BuildTags();

            /* ASSERT */
            for (int i = 0; i < expected.Length; i++)
            {
                int actual = builder.Tags[i].Id;
                Assert.AreEqual(expected[i], actual);
            }
        }

        private void SetupMockRepositoryFactoryToReturnTags()
        {
            this.mockRepositoryFactory.Setup(factory => factory.GetCrudRepository<Tag>()
                                                     .Get(It.IsAny<int>()))
                                                     .Returns((int id) => new Tag(id, "TestTag"));
        }

        private void SetProductTagsInMockProductEntity(int[] expected)
        {
            IList<ProductTagEntity> productTagsList = CreateProductTags(expected);
            this.mockProductEntity.Setup(pe => pe.ProductTagEntities).Returns(productTagsList);
        }

        private IList<ProductTagEntity> CreateProductTags(int[] expected)
        {
            IList<ProductTagEntity> productTagsList = new List<ProductTagEntity>();
            for (int i = 0; i < expected.Length; i++)
            {
                productTagsList.Add(new ProductTagEntity { TagId = expected[i] });
            }
            return productTagsList;
        }

        [TestMethod()]
        public void BuildStocks_ShouldSet_IdsInStocks_ToMatch_IdsInStockEntities()
        {
            /* ARRANGE */
            int[] expected = { 111, 222, 333 };
            GiveStockEntitiesToMockProductEntity(expected);
            SetupMockRepositoryFactoryToReturnStocks();

            /* ACT */
            ProductBuilder builder = CreateProductBuilderWithMocks();
            builder.BuildStocks();

            /* ASSERT */
            for (int i = 0; i < expected.Length; i++)
            {
                int actual = builder.Stocks[i].Id;
                Assert.AreEqual(expected[i], actual);
            }
        }

        private void GiveStockEntitiesToMockProductEntity(int[] expected)
        {
            IList<StockEntity> stockEntitiesList = CreateStockEntities(expected);
            this.mockProductEntity.Setup(pe => pe.StockEntities).Returns(stockEntitiesList);
        }

        private static IList<StockEntity> CreateStockEntities(int[] expected)
        {
            IList<StockEntity> stockEntitiesList = new List<StockEntity>();
            for (int i = 0; i < expected.Length; i++)
            {
                stockEntitiesList.Add(new StockEntity { Id = expected[i] });
            }
            return stockEntitiesList;
        }


        private void SetupMockRepositoryFactoryToReturnStocks()
        {
            this.mockRepositoryFactory.Setup(rf => rf.GetCrudRepository<Stock>()
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
            ProductBuilder builder = CreateProductBuilderWithMocks();
            IProduct product = builder.GetResult();

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
            GiveStockEntitiesToMockProductEntity(expectedStockIds);
            SetupMockDomainFactoryToReturnProduct();
            SetupMockRepositoryFactoryToReturnStocks();

            /* ACT */
            ProductBuilder builder = CreateProductBuilderWithMocks();
            builder.BuildStocks();
            IProduct product = builder.GetResult();

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
            SetProductTagsInMockProductEntity(expectedTagIds);
            SetupMockDomainFactoryToReturnProduct();
            SetupMockRepositoryFactoryToReturnTags();

            /* ACT */
            ProductBuilder builder = CreateProductBuilderWithMocks();
            builder.BuildTags();
            IProduct product = builder.GetResult();

            /* ASSERT */
            for (int i = 0; i < expectedTagIds.Length; i++)
            {
                int actual = product.Tags[i].Id;
                Assert.AreEqual(expectedTagIds[i], actual);
            }
        }
    }
}