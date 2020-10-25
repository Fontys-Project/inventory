using System.Collections.Generic;

namespace InventoryLogic.Stocks
{
    public interface IStockDAO
    { 
        Stock Get(int id);
        List<Stock> GetAll();
        void Add(Stock stock);
        void Remove(int id);
        void Modify(Stock stock, int id);
    }
}
