using System;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;
using InventoryLogic.Tags;

namespace InventoryLogic.Facade
{
    public class ProductsFacade : CrudDTOFacade<Product,ProductDTO>
    {
        public ProductsFacade(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
        }

        public List<ProductDTO> GetAllWith(int tagId)
        {
            List<ProductDTO> newViews = new List<ProductDTO>();
            List<Product> records = repoFactory.ProductsRepository.GetAllWith(tagId);

            foreach (Product record in records)
            {
                ProductDTO newView = new ProductDTO();
                record.ConvertToDTO(newView);
                newViews.Add(newView);
            }

            return newViews;
        }
    }
}
