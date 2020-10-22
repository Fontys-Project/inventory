using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Product
{
    public interface IProductDAO
    {
        Product GetProduct(int ID);
        List<Product> GetAllProducts();
        void AddProduct(Product product);
        void RemoveProduct(int ID);
        void ModifyProduct(Product product, int id);

    }
}
