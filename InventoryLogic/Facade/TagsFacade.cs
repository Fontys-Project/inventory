using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using System;

namespace InventoryLogic.Facade
{
    public class TagsFacade : CrudDTOFacade<Tag, TagDTO>
    {
        public TagsFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }
    }
}
