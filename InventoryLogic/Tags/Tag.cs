using InventoryLogic.Facade;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public class Tag : ITag, IDataAssignable<TagDTO>, IHasUniqueObjectId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constructor used by .net API framwork
        public Tag()
        {

        }

        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void ConvertFromDTO(TagDTO tagDTO)
        {
            this.Id = tagDTO.Id;
            this.Name = tagDTO.Name;
        }

        public void ConvertToDTO(TagDTO tagDTO)
        {
            tagDTO.Id = this.Id;
            tagDTO.Name = this.Name;
        }
    }
}
