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
        private readonly Stock stock;

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public ProductEntity ProductEntity { get; set; }

        public StockEntityBuilder(Stock stock, IEntityFactory entityFactory, IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.stock = stock;
            
            this.Id = stock.Id;
            this.ProductId = stock.ProductId;
            this.Amount = stock.Amount;
            this.Date = stock.Date;
        }

        public void BuildProductEntity()
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(stock.ProductId);
            this.ProductEntity = productEntity /*?? throw new InvalidDataException("Product not found. Please first create the product.")*/;
        }

        public StockEntity GetResult()
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
