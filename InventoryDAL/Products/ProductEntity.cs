using InventoryDAL.Database;
using InventoryDAL.ProductTag;
using InventoryDAL.ProductTag;
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
        public List<ProductTagEntity> ProductTagEntities { get; set; }
        public List<StockEntity> StockEntities { get; set; }

        public ProductEntity()
        {
            StockEntities = new List<StockEntity>();
            ProductTagEntities = new List<ProductTagEntity>();
        }

        public void ConvertFromDomainModel(Product fromDomainModel, IDAOFactory databaseFactory)
        {
            Id = fromDomainModel.Id;
            Name = fromDomainModel.Name;
            Price = fromDomainModel.Price;
            Sku = fromDomainModel.Sku;

            // add newly added stock items
            fromDomainModel.Stocks.ForEach(s =>
            {
                StockEntity stockEntity = ((StockEntityMySQLDAO)databaseFactory.StockDAO).Table.Find(s.Id);
                if (!StockEntities.Contains(stockEntity))
                    StockEntities.Add(stockEntity);
            });

            // remove all removed stock items
            StockEntities.RemoveAll(e =>
            (!fromDomainModel.Stocks.Any(ex => (e != null && ex.Id == e.Id))
            ));

            // add newly added tags
            fromDomainModel.Tags.ForEach(t =>
            {
                ProductTagEntity tagEntity = ((ProductEntityMySQLDAO)databaseFactory.ProductDAO)
                                .ProductTagsTable.Where(r => (r.TagId == t.Id && r.ProductId == fromDomainModel.Id)).FirstOrDefault();
                if(tagEntity == null)
                {
                    tagEntity = new ProductTagEntity(); //TODO: !
                    tagEntity.ProductId = Id;
                    tagEntity.ProductEntity = this;
                    tagEntity.TagId = t.Id;
                    tagEntity.TagEntity = ((TagEntityMySQLDAO)databaseFactory.TagDAO).Table.Find(t.Id);
                    ((ProductEntityMySQLDAO)databaseFactory.ProductDAO).dbContext.Add(tagEntity);
                    ((ProductEntityMySQLDAO)databaseFactory.ProductDAO).dbContext.SaveChangesAsync();
                    ProductTagEntities.Add(tagEntity);
                }
            });

            //remove all removed tags
            ProductTagEntities.ForEach(t =>
            {
                if (!fromDomainModel.Tags.All(e => (e.Id == t.TagId)))
                {
                    ((ProductEntityMySQLDAO)databaseFactory.ProductDAO).dbContext.Remove(t);
                }

            });
                
        }

        public void ConvertToDomainModel(Product toDomainModel, IDAOFactory databaseFactory)
        {
            toDomainModel.Id = Id;
            toDomainModel.Name = Name;
            toDomainModel.Price = Price;
            toDomainModel.Sku = Sku;

            // add stocks to domain model
            StockEntities.ForEach(s =>
            {
                if (!toDomainModel.Stocks.Any(e => (e.Id == s.Id) ))
                {
                    Stock stock = new Stock(s.Id,toDomainModel,s.Amount);
                    stock.Date = s.Date;
                    toDomainModel.Stocks.Add(stock);
                }
            });

            // remove stocks from domain model
            toDomainModel.Stocks.RemoveAll(e => (!StockEntities.Any(s => (s.Id == e.Id) )));

        }
    }
}
