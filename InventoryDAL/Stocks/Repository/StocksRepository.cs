using InventoryDAL.Interfaces;
using InventoryLogic.Interfaces;
using InventoryLogic.Stocks;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.Stocks
{
    public class StocksRepository : IStocksRepository
    {
        private readonly IBuilderFactory builderFactory; 
        private readonly IStockEntityDAO stockEntityDAO;

        public StocksRepository(IStockEntityDAO stockEntityDAO, IBuilderFactory builderFactory)
        {
            this.builderFactory = builderFactory; 
            this.stockEntityDAO = stockEntityDAO;
        }

        public List<Stock> GetAllExcludingNavigationProperties()
        {
            List<StockEntity> stockEntities = stockEntityDAO.GetAllIncludingNavigationProperties();
            return stockEntities
                .Select(stockEntity => BuildStock(stockEntity, false))
                .ToList(); 
        }

        public List<Stock> GetAll()
        {
            List<StockEntity> stockEntities = stockEntityDAO.GetAllIncludingNavigationProperties();
            return stockEntities
                .Select(stockEntity => BuildStock(stockEntity, true))
                .ToList();
        }

        public Stock GetExcludingNavigationProperties(int id)
        {
            StockEntity stockEntity = stockEntityDAO.GetIncludingNavigationProperties(id);
            return BuildStock(stockEntity, false);
        }

        public Stock Get(int id)
        {
            StockEntity stockEntity = stockEntityDAO.GetIncludingNavigationProperties(id);
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