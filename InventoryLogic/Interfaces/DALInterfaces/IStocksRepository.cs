using InventoryLogic.Interfaces;
using InventoryLogic.Stocks;
using System.Collections.Generic;
using InventoryDAL.Interfaces;

namespace InventoryDAL.Stocks
{
    public interface IStocksRepository : IRepository<Stock>
    {
    }
}