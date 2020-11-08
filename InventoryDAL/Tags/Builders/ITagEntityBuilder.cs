using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public interface ITagEntityBuilder : ITagEntity
    {
        TagEntity Build();
    }
}