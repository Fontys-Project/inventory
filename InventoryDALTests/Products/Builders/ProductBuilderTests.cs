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

        private ProductBuilder CreateProductBuilderWithMocks()
        {
            return new ProductBuilder(mockProductEntity.Object,
                                      mockDomainFactory.Object,
                                      mockRepositoryFactory.Object);
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
        public void ProductBuilder_ShouldHaveSameEmpty_Tags_List_WhenCreated()
        {
            int expected = 0;

            ProductBuilder builder = CreateProductBuilderWithMocks();
            int actual = builder.Tags.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductBuilder_ShouldHaveSameEmpty_Stocks_List_WhenCreated()
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
            // setup mock ProductEntity ProductTagEntities
            int[] expected = { 111, 222, 333 };
            IList<ProductTagEntity> productTagsList = new List<ProductTagEntity>();
            for (int i = 0; i < expected.Length; i++)
            {
                productTagsList.Add(new ProductTagEntity { TagId = expected[i] });
            }
            this.mockProductEntity.Setup(pe => pe.ProductTagEntities).Returns(productTagsList);
            // setup mock RepositoryFactory to return a TagRepository that returns Tags
            this.mockRepositoryFactory.Setup(rf => rf.GetCrudRepository<Tag>()
                                                     .Get(It.IsAny<int>()))
                                                     .Returns((int id) => new Tag(id, "TestTag"));

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

        [TestMethod()]
        public void BuildStocks_ShouldSet_IdsInStocks_ToMatch_IdsInStockEntities()
        {
            /* ARRANGE */
            // setup mock ProductEntity StockEntities
            int[] expected = { 111, 222, 333 };
            IList<StockEntity> stockEntitiesList = new List<StockEntity>();
            for (int i = 0; i < expected.Length; i++)
            {
                stockEntitiesList.Add(new StockEntity { Id = expected[i] });
            }
            this.mockProductEntity.Setup(pe => pe.StockEntities).Returns(stockEntitiesList);
            // setup mock RepositoryFactory to return a StockRepository that returns Stocks
            this.mockRepositoryFactory.Setup(rf => rf.GetCrudRepository<Stock>()
                                                     .Get(It.IsAny<int>()))
                                                     .Returns((int id) => new Stock(id, 999, 888));

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

        [TestMethod()]
        public void GetResult_ShouldReturnProduct_WithSameBasicProperties_AsProductEntity()
        {
            /* ARRANGE */
            // setup expected
            Product expected = new Product(100, "Name", 10.00M, "Sku");
            // setup mock DomainFactory
            this.mockDomainFactory.Setup(df 
                => df.CreateProduct(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Decimal>(), It.IsAny<string>(),
                                    It.IsAny<List<Tag>>(), It.IsAny<List<Stock>>()))
                .Returns((int id, string name, Decimal price, string sku, List<Tag> tags, List<Stock> stocks)
                    => new Product(id, name, price, sku, tags, stocks));
            // setup mock ProductEntity
            this.mockProductEntity.Object.Id = expected.Id;
            this.mockProductEntity.Object.Name = expected.Name;
            this.mockProductEntity.Object.Price = expected.Price;
            this.mockProductEntity.Object.Sku = expected.Sku;


            /* ACT */
            ProductBuilder builder = CreateProductBuilderWithMocks();
            IProduct product = builder.GetResult();

            /* ASSERT */
            Assert.AreEqual(product.Id, expected.Id);
            Assert.AreEqual(product.Name, expected.Name);
            Assert.AreEqual(product.Price, expected.Price);
            Assert.AreEqual(product.Sku, expected.Sku);
        }
    }
}