using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using InventoryDAL.Interfaces;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using InventoryDAL.ProductTag;
using InventoryLogic.Tags;

namespace InventoryDAL.Tags.Tests
{
    [TestClass()]
    public class TagBuilderTests
    {
        private Mock<IDomainFactory> mockDomainFactory;
        private Mock<IRepositoryFactory> mockRepositoryFactory;
        private Mock<ITagEntity> mockTagEntity;

        [TestInitialize]
        public void CreateMocks()
        {
            this.mockDomainFactory = new Mock<IDomainFactory>();
            this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
            this.mockTagEntity = new Mock<ITagEntity>().SetupAllProperties();
            SetupMockDomainFactoryToReturnTag();
            mockTagEntity.Setup(tagMock => tagMock.ProductTagEntities).Returns(new List<ProductTagEntity>());
        }

        [TestMethod()]
        public void TagBuilder_ShouldHaveSame_Id_AsEntity_WhenCreated()
        {
            int expected = 123;
            mockTagEntity.Setup(entity => entity.Id).Returns(expected);

            TagConverter builder = CreateTagBuilderWithMocks();
            int actual = builder.Convert(mockTagEntity.Object, (a, b) => { }).Id;

            Assert.AreEqual(expected, actual);
        }


        private TagConverter CreateTagBuilderWithMocks()
        {
            return new TagConverter(mockDomainFactory.Object,
                                  mockRepositoryFactory.Object);
        }

        [TestMethod()]
        public void TagBuilder_ShouldHaveSame_Name_AsEntity_WhenCreated()
        {
            string expected = "Name";
            mockTagEntity.Setup(entity => entity.Name).Returns(expected);

            TagConverter builder = CreateTagBuilderWithMocks();
            string actual = builder.Convert(mockTagEntity.Object, (a, b) => { }).Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TagBuilder_ShouldHaveEmpty_Products_List_WhenCreated()
        {
            int expected = 0;

            TagConverter builder = CreateTagBuilderWithMocks();
            int actual = builder.Convert(mockTagEntity.Object, (a, b) => { }).Products.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BuildProducts_ShouldSet_ProductIds_ToMatchProductIdsIn_ProductTags()
        {
            /* ARRANGE */
            int[] expectedProductIds = { 111, 222, 333 };
            SetProductTagsInMockTagEntity(expectedProductIds);
            SetupMockRepositoryFactoryToReturnProducts();

            /* ACT */
            TagConverter builder = CreateTagBuilderWithMocks();
            //builder.BuildProducts();

            /* ASSERT */
            for (int i = 0; i < expectedProductIds.Length; i++)
            {
                int actual = builder.Convert(mockTagEntity.Object, (a, b) => { }).Products[i].Id;
                Assert.AreEqual(expectedProductIds[i], actual);
            }
        }

        private void SetProductTagsInMockTagEntity(int[] ids)
        {
            IList<ProductTagEntity> productTagsList = CreateProductTags(ids);
            this.mockTagEntity.Setup(entity => entity.ProductTagEntities).Returns(productTagsList);
        }

        private IList<ProductTagEntity> CreateProductTags(int[] ids)
        {
            IList<ProductTagEntity> productTagsList = new List<ProductTagEntity>();
            for (int i = 0; i < ids.Length; i++)
            {
                productTagsList.Add(new ProductTagEntity { ProductId = ids[i] });
            }
            return productTagsList;
        }

        private void SetupMockRepositoryFactoryToReturnProducts()
        {
            this.mockRepositoryFactory.Setup(factory => factory.ProductsRepository
                                                     .Get(It.IsAny<int>()))
                                                     .Returns((int id) => new Product(id, "Name", 10M, "Sku"));
        }

        [TestMethod()]
        public void GetResult_ShouldReturnTag_WithSameBasicProperties_AsTagEntity()
        {
            /* ARRANGE */
            TagEntity basicProps = new TagEntity { Id = 111, Name = "Name" };
            SetBasicPropertiesInMockTagEntity(basicProps);
            SetupMockDomainFactoryToReturnTag();

            /* ACT */
            TagConverter builder = CreateTagBuilderWithMocks();
            ITag tag = builder.Convert(basicProps, (a, b) => { });

            /* ASSERT */
            Assert.AreEqual(tag.Id, basicProps.Id);
            Assert.AreEqual(tag.Name, basicProps.Name);
        }

        private void SetBasicPropertiesInMockTagEntity(TagEntity props)
        {
            this.mockTagEntity.Object.Id = props.Id;
            this.mockTagEntity.Object.Name = props.Name;
        }

        private void SetupMockDomainFactoryToReturnTag()
        {
            this.mockDomainFactory.Setup(factory => factory.CreateTag(It.IsAny<int>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<List<Product>>()))
                .Returns((int id, string name, List<Product> products) => new Tag(id, name, products));
        }


        [TestMethod()]
        public void GetResult_ShouldReturnTag_WithSameProductAssociations_AsTagEntity_WhenUsedAfter_BuildProducts()
        {
            /* ARRANGE */
            TagEntity basicProps = new TagEntity { Id = 111, Name = "Name" };
            int[] expectedProductIds = { 111, 222, 333 };
            SetBasicPropertiesInMockTagEntity(basicProps);
            SetProductTagsInMockTagEntity(expectedProductIds);
            SetupMockDomainFactoryToReturnTag();
            SetupMockRepositoryFactoryToReturnProducts();

            /* ACT */
            TagConverter builder = CreateTagBuilderWithMocks();
            //builder.BuildProducts();
            ITag tag = builder.Convert(mockTagEntity.Object, (a, b) => { });

            /* ASSERT */
            for (int i = 0; i < expectedProductIds.Length; i++)
            {
                int actual = tag.Products[i].Id;
                Assert.AreEqual(expectedProductIds[i], actual);
            }
        }
    }
}