using InventoryDAL.Products;
using InventoryLogic.Tags;

namespace InventoryDAL.Tags
{
    public class TagConverter
    {
        private readonly IDomainFactory domainFactory;

        public TagConverter(IDomainFactory factory) {
            this.domainFactory = factory;
        };

        public Tag ConvertToDomainTag(TagEntity e)
        {
            Tag tag = domainFactory.CreateTag();
            tag.Id = e.Id;
            tag.Name = e.Name;
            e.ProductTagJoinEntities.ForEach(j =>
            {
                ProductEntity productEntity = dbFactory.ProductDAO.Get(j.ProductId);
                tag.Products.Add(productEntity.ConvertToDomainModel());
            });

            //this.ProductTagJoinEntities.ForEach(j => {
            //    ProductEntity productEntity = factory.ProductDAO.Get(j.ProductId);
            //    toDomainModel.Products.Add(productEntity.ConvertToDomainModel());
            //});



        }

        public void ConvertToTagEntity(Tag tag)
        {
            toDomainModel.Id = this.Id;
            toDomainModel.Name = this.Name;

            //fromDomainModel.Products.ForEach(p => {
            //    ProductTagJoinEntity join = factory.ProductTagJoinDAO.Get(p.Id, this.Id);
            //    this.ProductTagJoinEntities.Add(join);
            //});

        }
    }
}
