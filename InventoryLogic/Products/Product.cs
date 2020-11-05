using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using InventoryLogic.Facade;
using InventoryLogic.ProductTag;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryLogic.Products
{
    public class Product : DomainModel<Product>, IProduct, IDataAssignable<ProductDTO>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public List<Tag> Tags { get; private set; }
        public List<Stock> Stocks { get; private set; }

        public Product()
        {
            Stocks = new List<Stock>();
            Tags = new List<Tag>();
        }

        public Product(int id, string name, decimal price, string sku) 
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
            Stocks = new List<Stock>();
            Tags = new List<Tag>();
        }

        public void ConvertFromDTO(ProductDTO fromDTO)
        {
            Name = fromDTO.Name;
            Price = fromDTO.Price;
            Sku = fromDTO.Sku;

            foreach(StockDTO stock in fromDTO.Stocks)
            {
                Stock stockModel = new Stock();
                stockModel.ConvertFromDTO(stock);
                if (!Stocks.Contains(stockModel))
                    Stocks.Add(stockModel);
            }

        }

        public void ConvertToDTO(ProductDTO toDTO)
        {
            toDTO.Name = Name;
            toDTO.Price = Price;
            toDTO.Id = Id;
            toDTO.Sku = Sku;
            toDTO.Stocks.Clear();
            foreach (Stock stock in Stocks)
            {
                StockDTO newStockDTO = new StockDTO();
                stock.ConvertToDTO(newStockDTO);

                toDTO.Stocks.Add(newStockDTO);
            }
            
        }

       
    }
}
