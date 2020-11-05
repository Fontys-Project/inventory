using System.Collections.Generic;
using System;
using InventoryDAL.Interfaces;

namespace InventoryDAL.Stocks
{
    public class StockEntityMockDAO : IStockEntityDAO
    {
        private readonly List<StockEntity> stockEntities;

        public StockEntityMockDAO()
        {
            stockEntities = new List<StockEntity>
            {
                new StockEntity(1, new ProductEntity(1,"ff",1,"f"), 25),
                new StockEntity(2, new ProductEntity(1,"ff",1,"f"), 15),
                new StockEntity(3, new ProductEntity(1,"ff",1,"f"), 10),
            };
        }

        public void Add(StockEntity stockEntity)
        {
            this.stockEntities.Add(stockEntity);
        }

        public List<StockEntity> GetAll()
        {
            return this.stockEntities;
        }

        public StockEntity Get(int id)
        {
            foreach (StockEntity e in this.stockEntities)
            {
                if (e.Id == id)
                    return e;
            }
            return null;
        }

        public void Modify(StockEntity e)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
