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

            if (e.ProductTagEntities != null)
            {
                e.ProductTagEntities.ForEach(prodTag =>
                {
                    TagEntity tagEntity = daoFactory.TagEntityDAO.Get(prodTag.TagId);
                    Tag tag = converterFactory.TagConverter.ConvertToTag(tagEntity);
                    product.Tags.Add(tag);
                });
            }
            if (e.StockEntities != null)
            {
                e.StockEntities.ForEach(e =>
                {
                    Stock stock = converterFactory.StockConverter.ConvertToStock(e);
                    product.Stocks.Add(stock);
                });
            }
            return product;
        }

        public ProductEntity ConvertToExistingProductEntity(Product product)
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(product.Id);
            if (productEntity == null) throw new InvalidDataException("ProductEntity by that id not found");

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;
            TransferTagsFromAToB(product, productEntity);
            TransferStocksFromAToB(product, productEntity);
            return productEntity;
        }

        private void TransferTagsFromAToB(Product a, ProductEntity b)
        {
            if (!(a.Tags == null || a.Tags.Count == 0))
            {
                b.ProductTagEntities = new List<ProductTagEntity>();
                a.Tags.ForEach(tag =>
                {
                    ProductTagEntity e = daoFactory.ProductTagDAO.Get(a.Id, tag.Id);
                    if (e == null)
                    {
                        e = entityFactory.CreateProductTagEntity();
                        e.ProductId = a.Id;
                        e.TagId = tag.Id;
                    }
                    b.ProductTagEntities.Add(e);
                });
            }
        }

        private void TransferStocksFromAToB(Product a, ProductEntity b)
        {
            if (!(a.Stocks == null || a.Stocks.Count == 0))
            {
                b.StockEntities = new List<StockEntity>();
                a.Stocks.ForEach(s =>
                {
                    StockEntity e = daoFactory.StockEntityDAO.Get(s.Id);
                    if (e == null) throw new InvalidDataException("Stock not found. You cannot add stocks this way.");
                    b.StockEntities.Add(e);
                });
            }
        }

        public ProductEntity ConvertToNewProductEntity(Product product)
        {
            ProductEntity productEntity = entityFactory.CreateProductEntity();
            if (!(product.Tags == null || product.Tags.Count == 0))
                throw new InvalidDataException("Cannot add tags this way. Please add tags separately.");
            if (!(product.Stocks == null || product.Stocks.Count == 0))
                throw new InvalidDataException("Cannot add stocks this way. Please add stocks separately.");

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;

            return productEntity;
        }
    }
}
