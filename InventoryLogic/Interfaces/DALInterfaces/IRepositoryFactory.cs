using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Products;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;

namespace InventoryLogic.Interfaces
{
    public interface IRepositoryFactory
    {
        IProductsRepository ProductsRepository { get; }
        IStocksRepository StocksRepository { get; }
        ITagsRepository TagsRepository { get; }
        
        IHasCrudActions<T> GetCrudRepository<T>();
    }
}
