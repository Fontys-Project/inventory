using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Product
{
    interface IProductDAO
    {
        IProduct GetProduct(int ID);
        List<IProduct> GetAllProducts();
        void AddProduct(IProduct product);
        void RemoveProduct(int ID);
        void ModifyProduct(IProduct product);

    }
}
