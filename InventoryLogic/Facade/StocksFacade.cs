using System;
using System.Collections.Generic;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
    public class StocksFacade : IFacade<Stock>
    {
        private readonly IDatabaseFactory databaseFactory;

        public StocksFacade(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Stock Get(int id)
        {
            return databaseFactory.GetStockDAO().Get(id);
        }


        public List<Stock> GetAll()
        {
            return databaseFactory.GetStockDAO().GetAll();
        }

        public Stock Add(Stock stock)
        {
            databaseFactory.GetStockDAO().Add(stock);
            return stock;
        }

        public Boolean Remove(int id)
        {
            databaseFactory.GetStockDAO().Remove(id);
            return true;
        }

        public Boolean Modify(Stock stock)
        {
            databaseFactory.GetStockDAO().Modify(stock);
            return true;
        }
    }
}

