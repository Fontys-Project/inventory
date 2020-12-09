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
    public class StockEntityConverter : IStockEntityConverter
    {
        private readonly IEntityFactory entityFactory;
        private readonly IDAOFactory daoFactory;

        public StockEntityConverter(IEntityFactory entityFactory, IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            
        }

        public StockEntity Convert(Stock stock)
        {
            StockEntity stockEntity = daoFactory.StockEntityDAO.Get(stock.Id);
            if (stockEntity == null) stockEntity = entityFactory.CreateStockEntity();
            stockEntity.ProductId = stock.ProductId;
            stockEntity.Amount = stock.Amount;
            stockEntity.Date = stock.Date;
            stockEntity.ProductEntity = daoFactory.ProductEntityDAO.Get(stock.ProductId);
            return stockEntity;
        }

    }
}
