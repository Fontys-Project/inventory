using System.Collections.Generic;
using InventoryDAL.Interfaces;
using InventoryLogic.Products;

namespace InventoryDAL.Products.PropertyProducts.Interfaces
{
    public interface IPropertyProductSupplier
    {
        IList<PropertyProduct> GetAll();
        PropertyProduct Get(int id);
    }
}
