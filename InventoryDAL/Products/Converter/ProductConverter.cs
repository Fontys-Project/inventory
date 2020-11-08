using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Products
{
    public class ProductConverter : IProductConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IConverterFactory converterFactory;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IEntityFactory entityFactory;

        public ProductConverter(IDomainFactory domainFactory,
                                IEntityFactory entityFactory,
                                IDAOFactory daoFactory,
                                IConverterFactory converterFactory,
                                IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.converterFactory = converterFactory;
            this.repositoryFactory = repositoryFactory;
        }

        public Product ConvertToProduct(ProductEntity e)
        {
            Product product = domainFactory.CreateProduct();
            product.Id = e.Id;
            product.Name = e.Name;
            product.Price = e.Price;
            product.Sku = e.Sku;
            product.Tags = GetTags(e.ProductTagEntities) ?? null;
            product.Stocks = GetStocks(e.StockEntities) ?? null;
            return product;
        }

        private List<Tag> GetTags(List<ProductTagEntity> productTagEntities)
        {
            List<Tag> tags = new List<Tag>();
            productTagEntities.ForEach(prodTag =>
            {
                Tag tag = repositoryFactory.GetCrudRepository<Tag>().Get(prodTag.TagId);
                tags.Add(tag);
            });
            return tags;
        }

        private List<Stock> GetStocks(List<StockEntity> stockEntities)
        {
            List<Stock> stocks = new List<Stock>();
            stockEntities.ForEach(stockEntity =>
            {
                Stock stock = repositoryFactory.GetCrudRepository<Stock>().Get(stockEntity.Id);
                stocks.Add(stock);
            });
            return stocks;
        }

        public ProductEntity ConvertToProductEntity(Product product)
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(product.Id);
            if (productEntity == null) productEntity = entityFactory.CreateProductEntity();

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;
            productEntity.ProductTagEntities = GetProductTagEntities(product.Id, product.Tags);
            productEntity.StockEntities = GetStockEntities(product.Stocks);

            return productEntity;
        }

        private List<ProductTagEntity> GetProductTagEntities(int productId, List<Tag> tags)
        {
            if (tags == null || tags.Count == 0) return null;
            List<ProductTagEntity> newProductTagEntities = new List<ProductTagEntity>();
            tags.ForEach(tag =>
            {
                ProductTagEntity ptEntity = GetProductTagEntity(productId, tag);
                newProductTagEntities.Add(ptEntity);
            });
            return newProductTagEntities;
        }

        private ProductTagEntity GetProductTagEntity(int productId, Tag tag)
        {
            ProductTagEntity ptEntity = daoFactory.ProductTagDAO.Get(productId, tag.Id);
            if (ptEntity == null) ptEntity = CreateProductTagEntity(productId, tag);
            return ptEntity;
        }

        private ProductTagEntity CreateProductTagEntity(int productId, Tag tag)
        {
            ProductTagEntity ptEntity = entityFactory.CreateProductTagEntity();
            ptEntity.ProductId = productId;
            ptEntity.TagId = tag.Id;
            return ptEntity;
        }

        private List<StockEntity> GetStockEntities(List<Stock> stocks)
        {
            if (stocks == null || stocks.Count == 0) return null;
            var stockEntities = new List<StockEntity>();
            stocks.ForEach(stock =>
            {
                StockEntity stockEntity = converterFactory.StockConverter.ConvertToStockEntity(stock);
                stockEntities.Add(stockEntity);
            });
            return stockEntities;
        }
    }
}
