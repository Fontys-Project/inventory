using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public interface ITagBuilder
    {
        Tag ConvertToTag(TagEntity e);
        TagEntity ConvertToNewTagEntity(Tag tag);
        TagEntity ConvertToExistingTagEntity(Tag tag);
    }
}