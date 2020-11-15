using InventoryLogic.Interfaces;
using InventoryLogic.Stocks;
using System.Collections.Generic;

namespace InventoryDAL.Stocks
{
    public interface IStocksRepository : ICrudRepository<Stock>
    {
    }
}