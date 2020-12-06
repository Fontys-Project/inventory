using System;
using System.Collections.Generic;
using System.Text;
using InventoryDAL.Interfaces;
using InventoryLogic.Tags;

namespace InventoryDAL.Tags.Repository
{
    public interface IPropertyTagSupplier : ICanExcludeNavigation<Tag>
    {
    }
}
