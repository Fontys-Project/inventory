using System.Collections.Generic;

namespace InventoryDAL.Stocks.PropertyStocks.Interfaces
{
    public interface IPropertyStockSupplier
    {
        IList<PropertyStock> GetAll();
        PropertyStock Get(int id);
    }
}
