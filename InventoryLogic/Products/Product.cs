using System.Collections.Generic;
using InventoryLogic.Facade;
using InventoryLogic.ProductTagJoins;
using InventoryLogic.Stocks;
using InventoryLogic.Tags;

namespace InventoryLogic.Products
{
    public class Product : IProduct, IDataAssignable<ProductDTO>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Sku { get; private set; }
        public List<ProductTagJoin> ProductTagJoins { get; private set; }
        public List<Stock> Stocks { get; private set; }

        public Product()
        {

        }

        public Product(int id, string name, decimal price, string sku)
        {
            Id = id;
            Name = name;
            Price = price;
            Sku = sku;
        }

        public void TransferDataFromView(ProductDTO fromView)
        {
            Name = fromView.Name;
            Price = fromView.Price;
            Sku = fromView.Sku;

        }

        public void TransferDataToView(ProductDTO toView)
        {
            toView.Name = Name;
            toView.Price = Price;
            toView.Id = Id;
            toView.Sku = Sku;
            toView.Stocks.Clear();
            foreach (Stock stock in Stocks)
            {
                toView.Stocks.Add(stock);
            }
            
        }
    }
}
