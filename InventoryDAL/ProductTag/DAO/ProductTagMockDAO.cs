
using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.ProductTag
{
    public class ProductTagMockDAO : IProductTagDAO
    {
        private readonly List<ProductTagEntity> joins;

        public ProductTagMockDAO()
        {
            joins = new List<ProductTagEntity>
            {
                new ProductTagEntity 
                {
                    ProductId = 1,
                    ProductEntity = new ProductEntity{ Id = 1, Name = "name", Price = 25, Sku = "sku" },
                    TagId = 1,
                    TagEntity = new TagEntity{ Id = 1, Name = "tagname" }
                }
            };
        }

        public ProductTagEntity Add(ProductTagEntity join)
        {
            joins.Add(join);
            return join;
        }

        public List<ProductTagEntity> GetAllWithNavigationProperties()
        {
            return joins;
        }

        public List<ProductTagEntity> GetAll()
        {
            return joins;
        }

        public ProductTagEntity Get(int joinId)
        {
            throw new NotImplementedException();
        }

        public ProductTagEntity Get(int productId, int tagId)
        {
            return joins.Where(j => j.ProductId == productId && j.TagId == tagId).First();
        }

        public void Modify(ProductTagEntity join)
        {
            throw new NotImplementedException();
        }

        public void Remove(int joinId)
        {
            throw new NotImplementedException();
        }

        public void Remove(int productId, int tagId)
        {
            throw new NotImplementedException();
        }
    }
}
