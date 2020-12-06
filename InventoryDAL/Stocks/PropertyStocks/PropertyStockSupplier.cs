using System.Collections.Generic;
using System.Linq;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Stocks.StockEntities.Interfaces;
using InventoryLogic.Stocks;

namespace InventoryDAL.Stocks.PropertyStocks
{
    public class PropertyStockSupplier : IPropertyStockSupplier
    {
        private readonly IStockEntityDAO dao;
        private readonly IPropertyStockConverter converter;

        public PropertyStockSupplier(IStockEntityDAO dao,
            IPropertyStockConverter converter)
        {
            this.dao = dao;
            this.converter = converter;
        }

        public List<PropertyStock> GetAll()
        {
            List<StockEntity> stockEntities = dao.GetAll();
            return stockEntities
                .Select(BuildStock)
                .ToList(); 
        }

        public PropertyStock Get(int id)
        {
            StockEntity stockEntity = dao.Get(id);
            return BuildStock(stockEntity);
        }

        private PropertyStock BuildStock(StockEntity stockEntity)
        {
            return converter.Convert(stockEntity);
        }
    }
}