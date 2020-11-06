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
        private readonly IDAOFactory daoFactory;

        public StockConverter(IDomainFactory domainFactory, IDAOFactory daoFactory)
        {
            this.domainFactory = domainFactory;
            this.daoFactory = daoFactory;
        }

        public Stock ConvertToStock(StockEntity e)
        {
            Stock stock = domainFactory.CreateStock();

            throw new NotImplementedException();
        }

        public StockEntity ConvertToStockEntity(Stock stock)
        {
            StockEntity entity = daoFactory.StockEntityDAO.Get(stock.Id);

            throw new NotImplementedException();
        }
    }
}
