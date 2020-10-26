using InventoryLogic.Products;
using InventoryLogic.ProductTagJoins;
using InventoryLogic.Tags;
using System;
using System.Linq;

namespace InventoryLogic.Facade
{
    public class TagsFacade : CrudFacade<Tag>
    {
        public TagsFacade(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool ApplyTag(int productId, int tagId)
        {
            Product product = databaseFactory.GetCrudDAO<Product>().Get(productId);
            if (product.ProductTagJoins == null || !product.ProductTagJoins.Where(j => j.TagId == tagId).Any())
            {
                ProductTagJoin join = new ProductTagJoin //TODO: better solution??
                {
                    ProductId = productId,
                    TagId = tagId
                };
                databaseFactory.ProductTagJoinDAO.Add(join);
                return true;
            }
            return false;
        }
    }
}
