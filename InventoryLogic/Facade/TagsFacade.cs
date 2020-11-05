using InventoryLogic.Products;
using InventoryLogic.ProductTag;
using InventoryLogic.Tags;
using System;
using System.Linq;

namespace InventoryLogic.Facade
{
    public class TagsFacade : CrudFacade<Tag>
    {
        public TagsFacade(IDAOFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool ApplyTag(int productId, int tagId)
        {
            Product product = databaseFactory.GetCrudDAO<Product>().Get(productId);
            Tag tag = databaseFactory.GetCrudDAO<Tag>().Get(tagId);
            if (!product.Tags.Contains(tag))
                product.Tags.Add(tag);

            databaseFactory.ProductDAO.Modify(product);
           
            return true;
        }
    }
}
