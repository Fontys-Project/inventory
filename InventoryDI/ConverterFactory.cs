using InventoryDAL.Tags;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Stocks;

namespace InventoryDI
{
    public class ConverterFactory : IConverterFactory
    {
        public ProductConverter ProductConverter { get; }
        public StockConverter StockConverter { get; }
        public TagConverter TagConverter { get; }

        public ConverterFactory(IDomainFactory domainFactory, IEntityFactory entityFactory, IDAOFactory daoFactory)
        {
            this.ProductConverter = new ProductConverter(domainFactory, entityFactory, daoFactory, this);
            this.StockConverter = new StockConverter(domainFactory, daoFactory);
            this.TagConverter = new TagConverter(domainFactory, entityFactory, daoFactory, this);
        }
    }
}
