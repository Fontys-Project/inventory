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

        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Sku { get; set; }
        public List<ProductTagEntity> ProductTagEntities { get; set; }
        public List<StockEntity> StockEntities { get; set; }

        public ProductEntityBuilder(Product product,
                                    IEntityFactory entityFactory,
                                    IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;

            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Sku = product.Sku;
            this.ProductTagEntities = GetProductTagEntities(product.Id, product.Tags);
            this.StockEntities = GetStockEntities(product.Stocks);
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
            if (ptEntity == null) throw new InvalidDataException("" +
                "Product-Tag relationship not found. " +
                "Please apply the tag using the dedicated method."); 
            return ptEntity;
        }

        private List<StockEntity> GetStockEntities(List<Stock> stocks)
        {
            if (stocks == null || stocks.Count == 0) return null;
            var stockEntities = new List<StockEntity>();
            stocks.ForEach(stock =>
            {
                StockEntity stockEntity = GetStockEntity(stock); 
                stockEntities.Add(stockEntity);
            });
            return stockEntities;
        }

        private StockEntity GetStockEntity(Stock stock)
        {
            StockEntity stockEntity = daoFactory.StockEntityDAO.Get(stock.Id);
            if (stockEntity == null) throw new InvalidDataException("Stock not found. Please first create this stock.");
            return stockEntity;
        }

        public ProductEntity Build()
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(this.Id); 
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
