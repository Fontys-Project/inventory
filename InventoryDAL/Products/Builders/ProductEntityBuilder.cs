using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Products
{
    public class ProductEntityBuilder : IProductEntityBuilder
    {
        private readonly IDAOFactory daoFactory;
        private readonly IEntityFactory entityFactory;
        private readonly Product product;

        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Sku { get; set; }
        public IList<ProductTagEntity> ProductTagEntities { get; set; }
        public IList<StockEntity> StockEntities { get; set; }

        public ProductEntityBuilder(Product product,
                                    IEntityFactory entityFactory,
                                    IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.product = product;

            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Sku = product.Sku;
            this.ProductTagEntities = new List<ProductTagEntity>();
            this.StockEntities = new List<StockEntity>();
        }

        public void BuildProductTagEntities()
        {
            List<Tag> tags = product.Tags;
            if (tags == null || tags.Count == 0) return;
            List<ProductTagEntity> newProductTagEntities = new List<ProductTagEntity>();
            tags.ForEach(tag =>
            {
                ProductTagEntity ptEntity = GetProductTagEntity(product.Id, tag.Id);
                newProductTagEntities.Add(ptEntity);
            });
            this.ProductTagEntities = newProductTagEntities;
        }

        private ProductTagEntity GetProductTagEntity(int productId, int tagId)
        {
            var ptEntity = daoFactory.ProductTagDAO.GetIncludingNavigationProperties(productId, tagId) ??
                           entityFactory.CreateProductTagEntity(productId, tagId, this.daoFactory);
            return ptEntity;
        }

        public void BuildStockEntities()
        {
            List<Stock> stocks = product.Stocks;
            if (stocks == null || stocks.Count == 0) return;
            var stockEntities = new List<StockEntity>();
            stocks.ForEach(stock =>
            {
                StockEntity stockEntity = GetStockEntity(stock.Id);
                stockEntities.Add(stockEntity);
            });
            this.StockEntities = stockEntities;
        }

        private StockEntity GetStockEntity(int stockId)
        {
            StockEntity stockEntity = daoFactory.StockEntityDAO.GetIncludingNavigationProperties(stockId);
            if (stockEntity == null) throw new InvalidDataException("Stock not found. Please first create this stock.");
            return stockEntity;
        }

        public ProductEntity GetResult()
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.GetIncludingNavigationProperties(this.Id);
            if (productEntity == null) productEntity = entityFactory.CreateProductEntity();
            productEntity.Name = this.Name;
            productEntity.Price = this.Price;
            productEntity.Sku = this.Sku;
            productEntity.ProductTagEntities = this.ProductTagEntities;
            productEntity.StockEntities = this.StockEntities;
            return productEntity;
        }
    }
}
