using InventoryDAL.Interfaces;
using InventoryLogic.Tags;

namespace InventoryDI
{
    public class DomainFactory : IDomainFactory
    {
        public Tag CreateTag()
        {
            return new Tag();
        }
    }
}
