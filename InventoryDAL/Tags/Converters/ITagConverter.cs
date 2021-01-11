using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public interface ITagConverter
    {

        public delegate void OnObjectCreation(Tag tag, ITagEntity tagEntity);

        public Tag Convert(ITagEntity tagEntity, OnObjectCreation onObjectCreation);
        
    }
}