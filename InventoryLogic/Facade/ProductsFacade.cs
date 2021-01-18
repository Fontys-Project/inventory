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

        public List<ProductDTO> GetAll(int tagId)
        {
            List<ProductDTO> dtos = new List<ProductDTO>();
            List<Product> products = repoFactory.ProductsRepository.GetAll(tagId);

            foreach (Product product in products)
            {
                ProductDTO newDto = new ProductDTO();
                product.ConvertToDTO(newDto);
                dtos.Add(newDto);
            }

            return dtos;
        }

        public bool ApplyTag(int productId, int tagId)
        {
            var productsRepo = repoFactory.ProductsRepository;
            var tagsRepo = repoFactory.TagsRepository;
            
            Product product = productsRepo.Get(productId);
            Tag tag = tagsRepo.Get(tagId);

            if (product == null) throw new ArgumentException("Product not found."); 
            if (tag == null) throw new ArgumentException("Tag not found.");
            if (product.Tags.Contains(tag)) return false;

            product.Tags.Add(tag);
            productsRepo.Modify(product);
            tagsRepo.RemoveFromCache(tag); // TODO: solve in DAL!

            return true;
        }

        public bool RemoveTag(int productId, int tagId)
        {
            var productsRepo = repoFactory.ProductsRepository;
            var tagsRepo = repoFactory.TagsRepository;

            Product product = productsRepo.Get(productId);
            Tag tag = tagsRepo.Get(tagId);

            if (product == null) throw new ArgumentException("Product not found.");
            if (tag == null) throw new ArgumentException("Tag not found.");
            if (!product.Tags.Contains(tag)) return false;

            product.Tags.Remove(tag);
            productsRepo.Modify(product);

            return true;
        }
    }
}
