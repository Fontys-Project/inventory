using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;

namespace InventoryDAL.Stocks
{
    public class StockConverter : IStockConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IEntityFactory entityFactory;
        private readonly IDAOFactory daoFactory;

        public StockConverter(IDomainFactory domainFactory, IEntityFactory entityFactory, IDAOFactory daoFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
        }

        public Stock ConvertToStock(StockEntity e)
        {
            Stock stock = domainFactory.CreateStock();

            throw new NotImplementedException();
        }

        public StockEntity ConvertToStockEntity(Stock stock)
        {
            StockEntity stockEntity = daoFactory.StockEntityDAO.Get(stock.Id);
            if (stockEntity == null) stockEntity = entityFactory.CreateStockEntity();
            stockEntity.ProductId = stock.ProductId;
            stockEntity.Date = stock.Date;
            stockEntity.Amount = stock.Amount;
            return stockEntity;
        }
    }
}
