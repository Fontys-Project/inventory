using System;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;

namespace InventoryLogic.Stocks
{
    public class Stock : IDataAssignable<StockDTO>, IHasUniqueObjectId
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Constructor used by .net API framwork
        public Stock()
        {

        }

        public Stock(int id, Product product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
            Date = DateTime.Today;
        }

        public void ConvertFromDTO(StockDTO fromView)
        {
            Id = fromView.Id;
            ProductId = fromView.ProductId;
            Product = fromView.Product;
            Amount = fromView.Amount;
            Date = fromView.Date;
            

        }

        public void ConvertToDTO(StockDTO toView)
        {
            toView.Id = Id;
            toView.ProductId = ProductId;
            toView.Product = Product;
            toView.Amount = Amount;
            toView.Date = Date;

        }
    }
}
