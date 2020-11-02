using System.Collections.Generic;
using InventoryLogic.Tags;
using InventoryDAL.ProductTagJoins;
using InventoryLogic.Facade;
using InventoryDAL.Database;
using InventoryDAL.Products;

namespace InventoryDAL.Tags
{
    public class TagEntity : IDomainModelAssignable<Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductTagJoinEntity> ProductTagJoinEntities { get; set; }

        public TagEntity()
        {
        }

        public void ConvertFromDomainModel(Tag fromDomainModel, IDatabaseFactory factory)
        {
            this.Id = fromDomainModel.Id;
            this.Name = fromDomainModel.Name;


            //fromDomainModel.Products.ForEach(p => {
            //    ProductTagJoinEntity join = factory.ProductTagJoinDAO.Get(p.Id, this.Id);
            //    this.ProductTagJoinEntities.Add(join);
            //});

        }

        public void ConvertToDomainModel(Tag toDomainModel, IDatabaseFactory factory)
        {
            toDomainModel.Id = this.Id;
            toDomainModel.Name = this.Name;            
            //this.ProductTagJoinEntities.ForEach(j => {
            //    ProductEntity productEntity = factory.ProductDAO.Get(j.ProductId);
            //    toDomainModel.Products.Add(productEntity.ConvertToDomainModel());
            //});
        }
    }
}
