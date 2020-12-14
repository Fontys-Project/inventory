using System.Collections.Generic;
using InventoryLogic.Facade;
using InventoryLogic.Interfaces;
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
        public List<Tag> Tags { get; set; }
        public List<Stock> Stocks { get; set; }

        public Product() // TODO: Remove. Means updating DTOFacade.
        { 
            Tags = new List<Tag>();
            Stocks = new List<Stock>();
        }

        public Product(int id, string name, decimal price, string sku)
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
            Tags = new List<Tag>();
            Stocks = new List<Stock>();
        }

        public Product(int id, string name, decimal price, string sku, List<Tag> tags, List<Stock> stocks)
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
            Tags = tags;
            Stocks = stocks;
        }

        public void ConvertFromDTO(ProductDTO fromDTO)
        {
            Name = fromDTO.Name;
            Price = fromDTO.Price;
            Sku = fromDTO.Sku;

            foreach (StockDTO stock in fromDTO.Stocks)
            {
                Stock stockModel = new Stock();
                stockModel.ConvertFromDTO(stock);
                if (!Stocks.Contains(stockModel))
                    Stocks.Add(stockModel);
            }
            foreach (TagDTO tag in fromDTO.Tags)
            {
                Tag tagModel = new Tag();
                tagModel.ConvertFromDTO(tag);
                if (!Tags.Contains(tagModel))
                    Tags.Add(tagModel);
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
            foreach (Tag tag in Tags)
            {
                TagDTO newTagDTO = new TagDTO();
                tag.ConvertToDTO(newTagDTO);

                toDTO.Tags.Add(newTagDTO);
            }

        }


    }
}
