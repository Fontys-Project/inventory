using InventoryDAL.Interfaces;
using InventoryDAL.ProductTag;
using InventoryDAL.Stocks;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using static InventoryDAL.Products.IProductConverter;

namespace InventoryDAL.Products
{
    public class ProductConverter : IProductConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IRepositoryFactory repositoryFactory;

        public ProductConverter(IDomainFactory domainFactory,
                              IRepositoryFactory repositoryFactory)
        {
            this.domainFactory = domainFactory;
            this.repositoryFactory = repositoryFactory;
        }
                

        public Product Convert(IProductEntity productEntity, OnObjectCreation onObjectCreation)
        {
            List<Tag> tags = new List<Tag>();
            List<Stock> stocks = new List<Stock>();
            //compose product
            Product product = new Product(productEntity.Id
                , productEntity.Name
                , productEntity.Price
                , productEntity.Sku,tags,stocks);

            // handle instantiated products. Needed by repository to prevent looping.
            onObjectCreation(product, productEntity);

            // gather child objects: tags
            foreach (ProductTagEntity productTagEntity in productEntity.ProductTagEntities)
            {
                Tag tag = repositoryFactory.TagsRepository.Get(productTagEntity.TagId);
                tags.Add(tag);
            }

            // gather child objects: stocks
            foreach (StockEntity stockEntity in productEntity.StockEntities)
            {
                Stock stock = repositoryFactory.StocksRepository.Get(stockEntity.Id);
                stocks.Add(stock);
            }
            

            return product;
            
        }

     
    }
}
