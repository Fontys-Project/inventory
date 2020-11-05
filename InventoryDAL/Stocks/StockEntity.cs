using InventoryDAL.Database;
using InventoryDAL.Products;
using InventoryLogic.Facade;
using InventoryLogic.Stocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryDAL.Stocks
{
    public class StockEntity : IDomainModelAssignable<Stock>
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Constructor used by .net API framwork
        public StockEntity()
        {

        }


        public void ConvertFromDomainModel(Stock fromDomainModel, IDAOFactory factory)
        {
            throw new NotImplementedException();
        }

        public void ConvertToDomainModel(Stock toDomainModel, IDAOFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
