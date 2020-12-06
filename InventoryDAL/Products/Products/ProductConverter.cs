using System;
using System.Collections.Generic;
using System.Linq;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.Products.Products.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks.PropertyStocks.Interfaces;
using InventoryDAL.Stocks.StockEntities;
using InventoryDAL.Tags.Repository;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryDAL.Products.Products
{
    public class ProductConverter : IProductConverter
    {
        private readonly IProductFactory productFactory;
        private readonly IPropertyStockSupplier propertyStockSupplier;
        private readonly IPropertyTagSupplier propertyTagSupplier;

        public ProductConverter(IProductFactory productFactory,
                              IPropertyStockSupplier propertyStockSupplier,
                              IPropertyTagSupplier propertyTagSupplier)
        {
            this.productFactory = productFactory;
            this.propertyStockSupplier = propertyStockSupplier;
            this.propertyTagSupplier = propertyTagSupplier;
        }

        public Product Convert(IProductEntity productEntity)
        {
            return productFactory.Create(productEntity.Id,
                                        productEntity.Name,
                                        productEntity.Price,
                                        productEntity.Sku,
                                        BuildTags(productEntity.ProductTagEntities),
                                        BuildStocks(productEntity.StockEntities)
            );
        }

        private List<Tag> BuildTags(IList<ProductTagEntity> productTagEntities)
        {
            List<Tag> tags = new List<Tag>();
            if (productTagEntities == null) return tags;
            tags.AddRange(productTagEntities.Select(GetTag));
            return tags;
        }

        private Tag GetTag(ProductTagEntity prodTag)
        {
            try
            {
                Tag tag = propertyTagSupplier.Get(prodTag.TagId);
                return tag;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Tag not found. Please first create the tag.", e);
            }
        }

        private List<Stock> BuildStocks(IList<StockEntity> stockEntities)
        {
            List<Stock> stocks = new List<Stock>();
            if (stockEntities == null) return stocks;
            stocks.AddRange(stockEntities.Select(GetStock));
            return stocks;
        }

        private Stock GetStock(StockEntity stockEntity)
        {
            try
            {
                Stock stock = propertyStockSupplier.Get(stockEntity.Id);
                return stock;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Stock not found. Please first create the stock.", e);
            }
        }
    }
}
