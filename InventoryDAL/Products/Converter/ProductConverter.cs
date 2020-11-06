using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryDAL.Tags;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;

namespace InventoryDAL.Products
{
    public class ProductConverter : IProductConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IConverterFactory converterFactory;

        public ProductConverter(IDomainFactory domainFactory, IDAOFactory daoFactory, IConverterFactory converterFactory)
        {
            this.domainFactory = domainFactory;
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

        public ProductEntity ConvertToProductEntity(Product product)
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(product.Id);
            productEntity.Id = product.Id;
            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;
            productEntity.ProductTagEntities.Clear();
            productEntity.StockEntities.Clear();

            product.Tags.ForEach(tag =>
            {
                ProductTagEntity prodTag = daoFactory.ProductTagDAO.Get(product.Id, tag.Id);
                productEntity.ProductTagEntities.Add(prodTag);
            });

            product.Stocks.ForEach(stock =>
            {
                StockEntity stockEntity = daoFactory.StockEntityDAO.Get(stock.Id);
                productEntity.StockEntities.Add(stockEntity);
            });

            return productEntity;
        }
    }
}
