using InventoryLogic.Facade;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.Products
{
    public class ProductDTO : IHasUniqueObjectId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<StockDTO> Stocks { get; set; }

        public ProductDTO()
        {
            Tags = new List<TagDTO>();
            Stocks = new List<StockDTO>();
        }
    }
}
