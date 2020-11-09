using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.IO;

namespace InventoryDAL.Stocks
{
    public class StockEntityBuilder : IStockEntityBuilder
    {
        private readonly IEntityFactory entityFactory;
        private readonly IDAOFactory daoFactory;

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public ProductEntity ProductEntity { get; set; }

        public StockEntityBuilder(Stock stock, IEntityFactory entityFactory, IDAOFactory daoFactory)
        {
            this.Id = stock.Id;
            this.ProductId = stock.ProductId;
            this.Amount = stock.Amount;
            this.Date = stock.Date;
            this.ProductEntity = GetProductEntity(stock.ProductId);

            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
        }

        private ProductEntity GetProductEntity(int productId)
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(productId);
            if (productEntity == null) throw new InvalidDataException("Product not found. Please first create the product.");
            return productEntity;
        }

        public StockEntity Build()
        {
            StockEntity stockEntity = daoFactory.StockEntityDAO.Get(this.Id);
            if (stockEntity == null) stockEntity = entityFactory.CreateStockEntity();
            stockEntity.ProductId = this.ProductId;
            stockEntity.Amount = this.Amount;
            stockEntity.Date = this.Date;
            stockEntity.ProductEntity = this.ProductEntity;
            return stockEntity;
        }
    }
}
