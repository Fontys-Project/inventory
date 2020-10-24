using System;
using System.Collections.Generic;
using System.Text;
using InventoryLogic.Products;
using InventoryLogic.ProductTags;
using InventoryLogic.Stocks;

namespace InventoryLogic.Facade
{
        public class StockFacade : IFacade<Stock>
        {
            private readonly IDatabaseFactory databaseFactory;

            public StockFacade(IDatabaseFactory databaseFactory)
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

            public Boolean Modify(Stock stock, int id)
            {
                databaseFactory.GetStockDAO().Modify(stock, id);
                return true;
            }
        }
}

