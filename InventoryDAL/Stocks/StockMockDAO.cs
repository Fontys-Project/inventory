using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using InventoryLogic.Stocks;
using InventoryLogic.Products;

namespace InventoryDAL.Stocks
{
    public class StockMockDAO : DbContext, IStockDAO
    {
        private readonly List<Stock> stocks;

        public StockMockDAO()
        {
            stocks = new List<Stock>
            {
                new Stock(1, new Product(1,"ff",1,"f"), 25),
                new Stock(2, new Product(1,"ff",1,"f"), 15),
                new Stock(3, new Product(1,"ff",1,"f"), 10),
            };
        }

        public void Add(Stock stock)
        {
            this.stocks.Add(stock);
        }

        public List<Stock> GetAll()
        {
            return this.stocks;
        }

        public Stock Get(int id)
        {
            foreach (Stock stock in this.stocks)
            {
                if (stock.Id == id)
                    return stock;
            }
            return null;
        }

        public void Modify(Stock stock)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
