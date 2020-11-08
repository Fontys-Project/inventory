using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;

namespace InventoryDAL.Stocks
{
    public class StockBuilder : IStockBuilder
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;

        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }


        public StockBuilder(StockEntity stockEntity, IDomainFactory domainFactory, IRepositoryFactory repositoryFactory)
        {
            this.Id = stockEntity.Id;
            this.ProductId = stockEntity.ProductId;
            this.Date = stockEntity.Date;
            this.Amount = stockEntity.Amount;
            this.Product = GetProduct(stockEntity.ProductId);

            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
        }

        private Product GetProduct(int productId)
        {
            return repositoryFactory.GetCrudRepository<Product>().Get(productId);
        }

        public Stock Build()
        {
            Stock stock = domainFactory.CreateStock();
            stock.Id = this.Id;
            stock.ProductId = this.ProductId;
            stock.Product = this.Product;
            stock.Amount = this.Amount;
            stock.Date = this.Date;
            return stock;
        }
    }
}

