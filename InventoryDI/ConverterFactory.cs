using InventoryLogic.Tags;
using InventoryDAL.Tags;

namespace InventoryDI.Converters
{
    class ConverterFactory
    {
        private readonly IDomainFactory domainFactory;

        public ConverterFactory(IDomainFactory domainFactory)
        {
            this.domainFactory = domainFactory;
        }
        
        public TagConverter CreateTagConverter()
        {
            return new TagConverter(domainFactory);
        }
    }
}
