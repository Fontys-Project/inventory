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

        public List<Stock> GetAll()
        {
            List<StockEntity> stockEntities = stockEntityDAO.GetAll();
            return stockEntities
                .Select(stockEntity => BuildStock(stockEntity))
                .ToList(); 
        }

        public Stock Get(int id)
        {
            StockEntity stockEntity = stockEntityDAO.Get(id);
            return BuildStock(stockEntity);
        }

        public Stock Add(Stock stock)
        {
            StockEntity stockEntity = BuildStockEntity(stock);
            stockEntity = stockEntityDAO.Add(stockEntity);
            return BuildStock(stockEntity);
        }

        public void Modify(Stock stock)
        {
            StockEntity stockEntity = BuildStockEntity(stock);
            stockEntityDAO.Modify(stockEntity);
        }

        public void Remove(int id)
        {
            stockEntityDAO.Remove(id);
        }

        private Stock BuildStock(StockEntity stockEntity)
        {
            var stockBuilder = builderFactory.CreateStockBuilder(stockEntity);
            return stockBuilder.Build();
        }

        private StockEntity BuildStockEntity(Stock stock)
        {
            var stockEntityBuilder = builderFactory.CreateStockEntityBuilder(stock);
            return stockEntityBuilder.Build();
        }
    }
}