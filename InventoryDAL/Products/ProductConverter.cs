using InventoryDAL.Interfaces;
using InventoryDAL.Stocks;
using InventoryLogic.Products;
using InventoryLogic.Stocks;

namespace InventoryDAL.Products
{
    public class ProductConverter
    {
        private readonly IDomainFactory domainFactory;
        private readonly IDAOFactory daoFactory;
        private readonly IConverterFactory converterFactory;

        public ProductConverter(IDomainFactory factory, IDAOFactory daoFactory, IConverterFactory converterFactory)
        {
            this.domainFactory = factory;
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

            e.StockEntities.ForEach(e =>
            {
                StockEntity stockEntity = daoFactory.StockEntityDAO.Get(e.StockId);
                Stock stock = converterFactory.StockConverter.ConvertToStock(stockEntity);
                product.Stocks.Add(stock);
            });
            return product;
        }

        public void ConvertToProductEntity(ProductEntity productEntity)
        {
            //toDomainModel.Id = this.Id;
            //toDomainModel.Name = this.Name;

            //fromDomainModel.Products.ForEach(p => {
            //    ProductTagJoinEntity join = factory.ProductTagJoinDAO.Get(p.Id, this.Id);
            //    this.ProductTagJoinEntities.Add(join);
            //});

        }
    }
}