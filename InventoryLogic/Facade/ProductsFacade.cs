using InventoryLogic.EventBus;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : CrudDTOFacade<Product, ProductDTO>
    {
        private readonly IEventBusMessenger busMessenger; // TODO: remove

        public ProductsFacade(IRepositoryFactory repoFactory, IEventBusMessenger busMessenger)
            : base(repoFactory)
        {
            this.busMessenger = busMessenger; // TODO: remove
        }


        //public override List<ProductDTO> GetAll() // TODO: remove
        //{
        //    busMessenger.Publish(new Message { Text = "lalalalala" });

        //    List<ProductDTO> newViews = new List<ProductDTO>();
        //    List<Product> records = repoFactory.GetCrudRepository<Product>().GetAll();

        //    foreach (Product record in records)
        //    {
        //        ProductDTO newView = new ProductDTO();
        //        record.ConvertToDTO(newView);
        //        newViews.Add(newView);
        //    }

        //    return newViews;
        //}
    }
}
