using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryDAL.Products
{
    public class ProductEntityConverter : IProductEntityConverter
    {
        private readonly IDAOFactory daoFactory;
        private readonly IEntityFactory entityFactory;
        public ProductEntityConverter(IEntityFactory entityFactory,
                                    IDAOFactory daoFactory)
        {
            this.entityFactory = entityFactory;
            this.daoFactory = daoFactory;
        }

        public ProductEntity Convert(Product product)
        {
            ProductEntity productEntity = daoFactory.ProductEntityDAO.Get(product.Id);
            if (productEntity == null) productEntity = entityFactory.CreateProductEntity();
            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Sku = product.Sku;


            productEntity.ProductTagEntities = new List<ProductTagEntity>();
            productEntity.StockEntities = new List<StockEntity>();

            product.Tags.ForEach(tag =>
            {
                ProductTagEntity ptEntity = daoFactory.ProductTagDAO.Get(product.Id, tag.Id) ??
                           entityFactory.CreateProductTagEntity(product.Id, tag.Id, this.daoFactory);
                productEntity.ProductTagEntities.Add(ptEntity);
            });

            product.Stocks.ForEach(stock =>
            {
                StockEntity stockEntity = daoFactory.StockEntityDAO.Get(stock.Id);
                if (stockEntity == null) throw new InvalidDataException("Stock not found. Please first create this stock."); // ?
                productEntity.StockEntities.Add(stockEntity);
            });

            return productEntity;
        }

    }
}
