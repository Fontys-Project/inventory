﻿using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;

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
            this.Id = productEntity.Id;
            this.Name = productEntity.Name;
            this.Price = productEntity.Price;
            this.Sku = productEntity.Sku;
            this.Tags = GetTags(productEntity.ProductTagEntities) ?? null;
            this.Stocks = GetStocks(productEntity.StockEntities) ?? null;
            
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
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
