using InventoryDAL.Tags;
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Stocks;

namespace InventoryDI
{
    class ConverterFactory : IConverterFactory
    {
        //public ProductConverter ProductConverter { get; }
        //public StockConverter StockConverter { get; }
        public TagConverter TagConverter { get; }

        public ConverterFactory(IDomainFactory domainFactory, IDAOFactory daoFactory)
        {
            //this.ProductConverter = new ProductConverter(domainFactory, daoFactory);
            //this.StockConverter = new StockConverter(domainFactory, daoFactory);
            this.TagConverter = new TagConverter(domainFactory, daoFactory, this);
        }
    }
}
