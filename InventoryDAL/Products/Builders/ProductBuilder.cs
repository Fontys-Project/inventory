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

        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Sku { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Stock> Stocks { get; set; }

        public ProductBuilder(ProductEntity productEntity,
                              IDomainFactory domainFactory,
                              IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
            
            this.Id = productEntity.Id;
            this.Name = productEntity.Name;
            this.Price = productEntity.Price;
            this.Sku = productEntity.Sku;
            this.Tags = GetTags(productEntity.ProductTagEntities);
            this.Stocks = GetStocks(productEntity.StockEntities);
        }

        private List<Tag> GetTags(List<ProductTagEntity> productTagEntities)
        {
            if (productTagEntities == null) return new List<Tag>();
            List<Tag> tags = new List<Tag>();
            productTagEntities.ForEach(prodTag =>
            {
                Tag tag = GetTag(prodTag);
                tags.Add(tag);
            });
            return tags;
        }

        private Tag GetTag(ProductTagEntity prodTag)
        {
            Tag tag = repositoryFactory.GetCrudRepository<Tag>().Get(prodTag.TagId);
            if (tag == null) throw new InvalidDataException("Tag not found. Please first create the tag.");
            return tag;
        }

        private List<Stock> GetStocks(List<StockEntity> stockEntities)
        {
            if (stockEntities == null) return new List<Stock>();
            List<Stock> stocks = new List<Stock>();
            stockEntities.ForEach(stockEntity =>
            {
                Stock stock = GetStock(stockEntity);
                stocks.Add(stock);
            });
            return stocks;
        }

        private Stock GetStock(StockEntity stockEntity)
        {
            Stock stock = repositoryFactory.GetCrudRepository<Stock>().Get(stockEntity.Id);
            if (stock == null) throw new InvalidDataException("Stock not found. Please first create the stock.");
            return stock;
        }

        public Product Build()
        {
            Product product = domainFactory.CreateProduct();
            product.Id = this.Id;
            product.Name = this.Name;
            product.Price = this.Price;
            product.Sku = this.Sku;
            product.Tags = this.Tags;
            product.Stocks = this.Stocks;
            return product;
        }
    }
}
