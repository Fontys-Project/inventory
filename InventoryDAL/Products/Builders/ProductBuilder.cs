using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Products
{
    public class ProductBuilder : IProductBuilder
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IProductEntity productEntity;

        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Sku { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Stock> Stocks { get; set; }

        public ProductBuilder(IProductEntity productEntity,
                              IDomainFactory domainFactory,
                              IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
            this.productEntity = productEntity;

            this.Id = productEntity.Id;
            this.Name = productEntity.Name;
            this.Price = productEntity.Price;
            this.Sku = productEntity.Sku;
            this.Tags = new List<Tag>();
            this.Stocks = new List<Stock>();
        }

        public void BuildTags()
        {
            IList<ProductTagEntity> productTagEntities = productEntity.ProductTagEntities;
            if (productTagEntities == null) return;
            List<Tag> tags = new List<Tag>();
            foreach (var prodTag in productTagEntities)
            {
                Tag tag = GetTag(prodTag);
                tags.Add(tag);
            }
            this.Tags = tags;
        }

        private Tag GetTag(ProductTagEntity prodTag)
        {
            try
            {
                Tag tag = repositoryFactory.TagsRepository.Get(prodTag.TagId);
                return tag;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Tag not found. Please first create the tag.", e);
            }
        }

        public void BuildStocks()
        {
            IList<StockEntity> stockEntities = productEntity.StockEntities;
            if (stockEntities == null) return;
            List<Stock> stocks = new List<Stock>();
            foreach (var stockEntity in stockEntities)
            {
                Stock stock = GetStock(stockEntity);
                stocks.Add(stock);
            }
            this.Stocks = stocks;
        }

        private Stock GetStock(StockEntity stockEntity)
        {
            try { 
                Stock stock = repositoryFactory.StocksRepository.Get(stockEntity.Id); 
                return stock; 
            }
            catch (NullReferenceException e) { 
                throw new NullReferenceException("Stock not found. Please first create the stock.", e); 
            }
        }

        public Product GetResult()
        {
            return domainFactory.CreateProduct(Id, Name, Price, Sku, Tags, Stocks);
        }
    }
}
