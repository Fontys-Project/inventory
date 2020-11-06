using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Products.ViewModels
{
    public class AddProductVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
    }
}
