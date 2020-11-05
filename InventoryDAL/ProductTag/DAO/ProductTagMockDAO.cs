
using InventoryDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.ProductTag
{
    public class ProductTagMockDAO : IProductTagDAO
    {
        private readonly List<InventoryLogic.ProductTag.ProductTag> joins;

        public ProductTagMockDAO()
        {
            joins = new List<InventoryLogic.ProductTag.ProductTag>
            {
                new InventoryLogic.ProductTag.ProductTag{
                    ProductId = 1,
                    Product = new Product(1, "name", 25, "sku"),
                    TagId = 1,
                    Tag = new Tag(1, "tagname")
                }
            };
        }

        public void Add(InventoryLogic.ProductTag.ProductTag join)
        {
            joins.Add(join);
        }

        public List<InventoryLogic.ProductTag.ProductTag> GetAll()
        {
            return joins;
        }

        public InventoryLogic.ProductTag.ProductTag Get(int joinId)
        {
            throw new NotImplementedException();
        }

        public InventoryLogic.ProductTag.ProductTag Get(int productId, int tagId)
        {
            return joins.Where(j => j.ProductId == productId && j.TagId == tagId).First();
        }

        public void Modify(InventoryLogic.ProductTag.ProductTag join)
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
