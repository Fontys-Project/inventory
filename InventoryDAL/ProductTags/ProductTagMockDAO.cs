using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryLogic.ProductTags;
using System;

namespace InventoryDAL.ProductTags
{
    public class ProductTagMockDAO : DbContext, IProductTagDAO
    {
        private List<ProductTag> productTags;

       public ProductTagMockDAO()
        {
            productTags = new List<ProductTag>();
            productTags.Add(new ProductTag(1, "Tag1"));
        }

        public void Add(ProductTag tag)
        {
            this.productTags.Add(tag);
        }

        public List<ProductTag> GetAll()
        {
            return this.productTags;
        }

        public ProductTag Get(int id)
        {
            foreach (ProductTag tag in this.productTags)
            {
                if (tag.Id == id)
                    return tag;
            }
            return null;
        }

        public void Modify(ProductTag tag)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
