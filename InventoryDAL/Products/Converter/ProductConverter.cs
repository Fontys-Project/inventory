using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace InventoryDAL.Products
{
    public class ProductConverter : IProductConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IConverterFactory converterFactory;
        private readonly IEntityFactory entityFactory;

        public ProductConverter(IDomainFactory domainFactory, IEntityFactory entityFactory, IDAOFactory daoFactory, IConverterFactory converterFactory)
        {
            this.domainFactory = domainFactory;
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
            this.converterFactory = converterFactory;
        }

        public Product ConvertToProduct(ProductEntity e)
        {
            Product product = domainFactory.CreateProduct();
            product.Id = e.Id;
            product.Name = e.Name;
            product.Price = e.Price;
            product.Sku = e.Sku;

            e.ProductTagEntities.ForEach(prodTag =>
            {
                TagEntity tagEntity = daoFactory.TagEntityDAO.Get(prodTag.TagId);
                Tag tag = converterFactory.TagConverter.ConvertToTag(tagEntity);
                product.Tags.Add(tag);
            });
            
            e.StockEntities.ForEach(e =>
            {
                Stock stock = converterFactory.StockConverter.ConvertToStock(e);
                product.Stocks.Add(stock);
            });

            return product;
        }

        public ProductEntity ConvertToExistingProductEntity(Product product)
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(product.Id);
            if (productEntity == null) throw new InvalidDataException("ProductEntity by that id not found"); 

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;

            // check if tags are different
            var tagIDs1 = product.Tags.Select(t => t.Id).ToList();
            var tagIDs2 = productEntity.ProductTagEntities.Select(pt => pt.TagId).ToList();
            if (tagIDs1.Except(tagIDs2).ToList().Any() || tagIDs2.Except(tagIDs1).ToList().Any())
                throw new InvalidDataException("Cannot change tags this way");

            // check if tags are different
            var tagIDs1 = product.Tags.Select(t => t.Id).ToList();
            var tagIDs2 = productEntity.ProductTagEntities.Select(pt => pt.TagId).ToList();
            if (tagIDs1.Except(tagIDs2).ToList().Any() || tagIDs2.Except(tagIDs1).ToList().Any())
                throw new InvalidDataException("Cannot change tags this way");

            //List<int> ids = product.Tags.Select(t => t.Id).ToList();
            //bool nomatch = ids.Contains(ids => productEntity.ProductTagEntities.Any()
            //product.Tags.Any(t => productEntity.ProductTagEntities.Any(pt => pt.Id == t.Id));


            // OLD

            //var newTags = product.Tags.Where(t => productEntity.ProductTagEntities.All(pt => pt.Id != t.Id)).ToList();
            //newTags.ForEach(newTag =>
            //{
            //    ProductTagEntity prodTag = daoFactory.ProductTagDAO.Get(product.Id, newTag.Id);
            //    productEntity.ProductTagEntities.Add(prodTag);
            //});

            //product.Stocks.ForEach(stock =>
            //{
            //    StockEntity stockEntity = daoFactory.StockEntityDAO.Get(stock.Id);
            //    productEntity.StockEntities.Add(stockEntity);
            //});

            return productEntity;
        }

        public ProductEntity ConvertToNewProductEntity(Product product)
        {
            ProductEntity productEntity = entityFactory.CreateProductEntity();

            productEntity.Id = product.Id;
            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;
            productEntity.ProductTagEntities = new List<ProductTagEntity>();
            productEntity.StockEntities = new List<StockEntity>();

            if (product.Tags != null) throw new InvalidDataException("You cannot attach tags this way");
            if (product.Stocks != null) throw new InvalidDataException("You cannot attach stocks this way");

            return productEntity;
        }
    }
}
