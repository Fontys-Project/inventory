using InventoryDAL.ProductTag;
using System.Collections.Generic;

namespace InventoryDAL.Tags
{
    public interface ITagEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        IList<ProductTagEntity> ProductTagEntities { get; set; }
    }
}