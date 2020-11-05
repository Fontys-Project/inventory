using InventoryLogic.Crud;
using InventoryLogic.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryDAL.Tags
{
    class TagRepository : ICrudRepository<Tag>
    {
        public DbSet<EntityType> Table { get; private set; }

        public TagRepository(ITagEntityDAO tagDAO)
        {
            this.dbContext = context;
            this.Table = context.Set<EntityType>();
        }

        public void Add(DomainModel obj)
        {
            EntityType entity = new EntityType();
            entity.ConvertFromDomainModel(obj, databaseFactory);

            this.dbContext.Database.EnsureCreated();
            this.Table.Add(entity);
            this.dbContext.SaveChangesAsync();
            entity.ConvertToDomainModel(obj, databaseFactory);
        }

        public List<DomainModel> GetAll()
        {
            List<DomainModel> domainObjs = new List<DomainModel>();

            this.dbContext.Database.EnsureCreated();
            Task<List<EntityType>> lst = this.Table.ToListAsync();
            lst.Wait(); // TODO: beter async uitwerken?
            foreach (EntityType entity in lst.Result)
            {
                DomainModel newDomainObj = new DomainModel();
                entity.ConvertToDomainModel(newDomainObj, databaseFactory);
                domainObjs.Add(newDomainObj);
            }

            return domainObjs;
        }

        public DomainModel Get(int id)
        {
            DomainModel model = new DomainModel();
            this.dbContext.Database.EnsureCreated();
            this.Table.Find(id).ConvertToDomainModel(model, databaseFactory);

            return model;
        }

        public void Modify(DomainModel obj)
        {
            EntityType entity = this.Table.Find(obj.Id);

            entity.ConvertFromDomainModel(obj, databaseFactory);

            this.dbContext.Database.EnsureCreated();
            this.dbContext.Update(entity);
            this.dbContext.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Remove(this.Table.Find(id));
            this.dbContext.SaveChangesAsync();
        }
    }
}
