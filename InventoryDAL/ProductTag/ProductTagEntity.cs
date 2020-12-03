using InventoryDAL.Interfaces;
using InventoryDAL.Products;
using InventoryDAL.Tags;

namespace InventoryDAL.ProductTag
{
    public class ProductTagEntity
    {
        public int ProductId { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public int TagId { get; set; }
        public TagEntity TagEntity { get; set; }

        public ProductTagEntity() 
        { 
        }

        public ProductTagEntity(int productId, int tagId, IDAOFactory daoFactory)
        {
            this.ProductId = productId;
            this.ProductEntity = daoFactory.ProductEntityDAO.GetIncludingNavigationProperties(productId);
            this.TagId = tagId;
            this.TagEntity = daoFactory.TagEntityDAO.GetIncludingNavigationProperties(tagId);
        }
    }
}
