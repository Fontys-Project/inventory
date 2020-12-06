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

        public Stock() { }  // TODO: Remove. Means updating DTOFacade.

        public Stock(int id, int productId, int amount)
        {
            Id = id;
            ProductId = productId;
            Amount = amount;
            Date = DateTime.Today;
        }

        public Stock(int id, int productId, int amount, DateTime date)
        {
            Id = id;
            ProductId = productId;
            Amount = amount;
            Date = date;
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
