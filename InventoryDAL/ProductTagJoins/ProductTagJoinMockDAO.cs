using InventoryLogic.Facade;
using InventoryLogic.Products;
using InventoryLogic.ProductTagJoins;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDAL.ProductTagJoins
{
    public class ProductTagJoinMockDAO : ICrudDAO<ProductTagJoin>
    {
        private readonly List<ProductTagJoin> joins;

        public ProductTagJoinMockDAO()
        {
            joins = new List<ProductTagJoin>
            {
                new ProductTagJoin{
                    ProductId = 1,
                    Product = new Product(1, "name", 25, "sku"),
                    TagId = 1,
                    Tag = new Tag(1, "tagname")
                }
            };
        }

        public void Add(ProductTagJoin join)
        {
            joins.Add(join);
        }

        public List<ProductTagJoin> GetAll()
        {
            return joins;
        }

        public ProductTagJoin Get(int joinId)
        {
            throw new NotImplementedException();
        }

        public ProductTagJoin Get(int productId, int tagId)
        {
            return joins.Where(j => j.ProductId == productId && j.TagId == tagId).First();
        }

        public void Modify(ProductTagJoin join)
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
