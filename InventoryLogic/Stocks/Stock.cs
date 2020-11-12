using System;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;

namespace InventoryLogic.Stocks
{
    public class Stock : IStock, IDataAssignable<StockDTO>, IHasUniqueObjectId
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Constructor used by .net API framwork
        public Stock()
        {

        }

        public Stock(int id, Product product, int amount)
        {
            Id = id;
            Amount = amount;
            Date = DateTime.Today;
        }

        public void ConvertFromDTO(StockDTO fromView)
        {
            Id = fromView.Id;
            ProductId = fromView.ProductId;
            Amount = fromView.Amount;
            Date = fromView.Date;


        }

        public void ConvertToDTO(StockDTO toView)
        {
            toView.Id = Id;
            toView.ProductId = ProductId;
            toView.Amount = Amount;
            toView.Date = Date;

        }
    }
}
