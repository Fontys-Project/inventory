using System;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;

namespace InventoryLogic.Stocks
{
    public class Stock : IStock, IDataAssignable<StockDTO>, IHasUniqueObjectId
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public Stock(int id, Product product, int amount)
        {
            Id = id;
            ProductId = product.Id;
            Product = product;
            Amount = amount;
            Date = DateTime.Today;
        }

        public Stock(int id, int productId, int amount, Product product = null)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            Amount = amount;
            Date = DateTime.Today;
        }

        public Stock(int id, int productId, int amount, DateTime date, Product product = null)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            Amount = amount;
            Date = date;
        }

        public void ConvertFromDTO(StockDTO fromView)
        {
            Id = fromView.Id;
            ProductId = fromView.ProductId;
            Product = new Product(fromView.Product.Id,fromView.Product.Name,fromView.Product.Price,fromView.Product.Sku);
            Product.ConvertFromDTO(fromView.Product);
            Amount = fromView.Amount;
            Date = fromView.Date;
        }

        public void ConvertToDTO(StockDTO toView)
        {
            toView.Id = Id;
            toView.ProductId = ProductId;
            toView.Product = new ProductDTO();
            toView.Product.Id = Product.Id;
            toView.Product.Name = Product.Name;
            toView.Product.Price = Product.Price;
            toView.Product.Sku = Product.Sku;
            toView.Amount = Amount;
            toView.Date = Date;

        }
    }
}
