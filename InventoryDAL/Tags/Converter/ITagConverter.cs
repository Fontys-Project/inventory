using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public interface ITagConverter
    {
        Tag ConvertToTag(TagEntity e);
        TagEntity ConvertToTagEntity(Tag tag);
    }
}