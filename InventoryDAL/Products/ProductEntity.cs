using InventoryDAL.Database;
using InventoryDAL.ProductTagJoins;
using InventoryDAL.ProductTags;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Facade;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryDAL.Products
{
    public class ProductEntity : IDomainModelAssignable<Product>
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public string Sku { get; set; }
        public List<ProductTagJoinEntity> ProductTagJoins { get; set; }
        public List<StockEntity> Stocks { get; set; }

        public ProductEntity()
        {
            Stocks = new List<StockEntity>();
            ProductTagJoins = new List<ProductTagJoinEntity>();
        }

        public void ConvertFromDomainModel(Product fromDomainModel, IDatabaseFactory databaseFactory)
        {
            Id = fromDomainModel.Id;
            Name = fromDomainModel.Name;
            Price = fromDomainModel.Price;
            Sku = fromDomainModel.Sku;

            // add newly added stock items
            fromDomainModel.Stocks.ForEach(s =>
            {
                StockEntity stockEntity = ((StockMySqlDAO)databaseFactory.StockDAO).Table.Find(s.Id);
                if (!Stocks.Contains(stockEntity))
                    Stocks.Add(stockEntity);
            });

            // remove all removed stock items
            Stocks.RemoveAll(e =>
            (!fromDomainModel.Stocks.Any(ex => (e != null && ex.Id == e.Id))
            ));

            // add newly added tags
            fromDomainModel.Tags.ForEach(t =>
            {
                ProductTagJoinEntity tagEntity = ((ProductMySQLDAO)databaseFactory.ProductDAO)
                                .ProductTagsTable.Where(r => (r.TagId == t.Id && r.ProductId == fromDomainModel.Id)).FirstOrDefault();
                if(tagEntity == null)
                {
                    tagEntity = new ProductTagJoinEntity();
                    tagEntity.ProductId = Id;
                    tagEntity.ProductEntity = this;
                    tagEntity.TagId = t.Id;
                    tagEntity.TagEntity = ((TagMySQLDAO)databaseFactory.TagDAO).Table.Find(t.Id);
                    ((ProductMySQLDAO)databaseFactory.ProductDAO).dbContext.Add(tagEntity);
                    ((ProductMySQLDAO)databaseFactory.ProductDAO).dbContext.SaveChangesAsync();
                    ProductTagJoins.Add(tagEntity);
                }

            });

            //remove all removed tags
            ProductTagJoins.ForEach(t =>
            {
                if (!fromDomainModel.Tags.All(e => (e.Id == t.TagId)))
                {
                    ((ProductMySQLDAO)databaseFactory.ProductDAO).dbContext.Remove(t);
                }

            });
                
        }

        public void ConvertToDomainModel(Product toDomainModel, IDatabaseFactory databaseFactory)
        {
            toDomainModel.Id = Id;
            toDomainModel.Name = Name;
            toDomainModel.Price = Price;
            toDomainModel.Sku = Sku;

            // add stocks to domain model
            Stocks.ForEach(s =>
            {
                if (!toDomainModel.Stocks.Any(e => (e.Id == s.Id) ))
                {
                    Stock stock = new Stock(s.Id,toDomainModel,s.Amount);
                    stock.Date = s.Date;
                    toDomainModel.Stocks.Add(stock);
                }
            });

            // remove stocks from domain model
            toDomainModel.Stocks.RemoveAll(e => (!Stocks.Any(s => (s.Id == e.Id) )));

        }
    }
}
