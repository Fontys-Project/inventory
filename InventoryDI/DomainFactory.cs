using InventoryLogic.Tags;

namespace InventoryDI
{
    class DomainFactory : IDomainFactory
    {
        public Tag CreateTag()
        {
            return new Tag();
        }
    }
}
