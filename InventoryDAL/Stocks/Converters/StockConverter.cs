using InventoryDAL.Interfaces;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Stocks
{
    public class StockConverter : IStockConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;


        public StockConverter(IDomainFactory domainFactory, IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
   
        }

        public Stock Convert(IStockEntity stockEntity, IStockConverter.OnObjectCreation onObjectCreation)
        {
            Stock stock = domainFactory.CreateStock(stockEntity.Id, stockEntity.ProductId, stockEntity.Amount, stockEntity.Date, 
                repositoryFactory.ProductsRepository.Get(stockEntity.ProductId));
            // trigger cache on instantiation to prevent looping.
            onObjectCreation(stock, stockEntity);
            return stock;
        }

    }
}

