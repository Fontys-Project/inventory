using System.Collections.Generic;
using System;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;

namespace InventoryDAL.Stocks
{
    public class StockEntityMockDAO : IStockEntityDAO
    {
        private readonly List<StockEntity> stockEntities;

        public StockEntityMockDAO()
        {
            stockEntities = new List<StockEntity>
            {
                new StockEntity
                {
                    Id = 1, 
                    ProductEntity = new ProductEntity{ Id = 1, Name = "ff", Price = 1, Sku = "f" }, 
                    Amount = 25 
                },
                new StockEntity
                {
                    Id = 2, 
                    ProductEntity = new ProductEntity{ Id = 1, Name = "ff", Price = 1, Sku = "f" }, 
                    Amount = 15 
                },
                new StockEntity
                {
                    Id = 3, 
                    ProductEntity = new ProductEntity{ Id = 1, Name = "ff", Price = 1, Sku = "f" }, 
                    Amount = 10 
                },
            };
        }

        public StockEntity Add(StockEntity stockEntity)
        {
            this.stockEntities.Add(stockEntity);
            return stockEntity;
        }

        public List<StockEntity> GetAllWithNavigationProperties()
        {
            return this.stockEntities;
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
