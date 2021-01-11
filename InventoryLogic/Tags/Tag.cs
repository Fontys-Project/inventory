using InventoryLogic.Facade;
using InventoryLogic.Interfaces;
using InventoryLogic.Products;
using System.Collections.Generic;

namespace InventoryLogic.Tags
{
    public class Tag : ITag, IDataAssignable<TagDTO>, IHasUniqueObjectId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        
        public Tag(int id, string name, List<Product> products = null)
        {
            Id = id;
            Name = name;
            Products = products ?? new List<Product>();
        }

        public void ConvertFromDTO(TagDTO tagDTO)
        {
            this.Id = tagDTO.Id;
            this.Name = tagDTO.Name;
            foreach (ProductDTO productDto in tagDTO.Products)
            {
                Product productModel = new Product(productDto.Id,productDto.Name,productDto.Price,productDto.Sku);
                productModel.ConvertFromDTO(productDto);
                if (!Products.Contains(productModel))
                    Products.Add(productModel);
            }

        }

        public void ConvertToDTO(TagDTO tagDTO)
        {
            tagDTO.Id = this.Id;
            tagDTO.Name = this.Name;

            tagDTO.Products.Clear();
            foreach (Product product in Products)
            {
                ProductDTO newDTO = new ProductDTO() // prevent logic layer loop.
                {
                    Id = product.Id,
                    Name = product.Name,
                    Sku = product.Sku,
                    Price = product.Price
                };

                tagDTO.Products.Add(newDTO);
            }
        }
    }
}
