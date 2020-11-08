using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public interface ITagBuilder : ITag
    {
        Tag Build();
    }
}