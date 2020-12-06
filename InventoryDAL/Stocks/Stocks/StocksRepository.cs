using System.Collections.Generic;
using System.Linq;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.StockEntities.Interfaces;
using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks.Stocks
{
    public class StocksRepository : IStocksRepository
    {
        private readonly IDAOFactory daoFactory;
        private readonly IBuilderFactory builderFactory; 
        private readonly IStockEntityDAO stockEntityDAO;

        public StocksRepository(IDAOFactory daoFactory, IBuilderFactory builderFactory)
        {
            this.builderFactory = builderFactory; 
            this.daoFactory = daoFactory;
            this.stockEntityDAO = daoFactory.StockEntityDAO;
        }

        public List<Stock> GetAll()
        {
            List<StockEntity> stockEntities = stockEntityDAO.GetAll();
            return stockEntities
                .Select(stockEntity => BuildStock(stockEntity, true))
                .ToList();
        }

        public Stock Get(int id)
        {
            StockEntity stockEntity = stockEntityDAO.Get(id);
            return BuildStock(stockEntity, true);
        }

        public Stock Add(Stock stock)
        {
            StockEntity stockEntity = BuildStockEntity(stock, false);
            stockEntity = stockEntityDAO.Add(stockEntity);
            return BuildStock(stockEntity, true);
        }

        public void Modify(Stock stock)
        {
            StockEntity stockEntity = BuildStockEntity(stock, false);
            stockEntityDAO.Modify(stockEntity);
        }

        public void Remove(int id)
        {
            stockEntityDAO.Remove(id);
        }

        private Stock BuildStock(StockEntity stockEntity, bool includesNavigationProperties)
        {
            var stockBuilder = builderFactory.CreateStockBuilder(stockEntity);
            if(includesNavigationProperties){
                stockBuilder.BuildProduct();
            }
            return stockBuilder.GetResult();
        }

        private StockEntity BuildStockEntity(Stock stock, bool includesNavigationProperties)
        {
            var stockEntityBuilder = builderFactory.CreateStockEntityBuilder(stock);
            if(includesNavigationProperties){
                stockEntityBuilder.BuildProductEntity();
            }
            return stockEntityBuilder.GetResult();
        }
    }
}