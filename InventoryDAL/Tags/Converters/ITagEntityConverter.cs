using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public interface ITagEntityConverter
    {
        TagEntity Convert(Tag tag);
    }
}