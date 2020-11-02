using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryLogic.Crud;
using InventoryLogic.Facade;

namespace InventoryDAL.Database
{
    public abstract class MySqlDAO<DomainModel, EntityType> : ICrudDAO<DomainModel> where EntityType : class, IDomainModelAssignable<DomainModel>, new() where DomainModel : IHasUniqueObjectId, new()
    {
        public readonly MySqlContext dbContext;
        public DbSet<EntityType> Table { get; private set; }

        private IDatabaseFactory databaseFactory { get; set; }

        public MySqlDAO(MySqlContext context, IDatabaseFactory databaseFactory)
        {
            this.dbContext = context;
            this.Table = context.Set<EntityType>();
            this.databaseFactory = databaseFactory;
        }

        public void Add(DomainModel obj)
        {
            EntityType entity = new EntityType();
            entity.ConvertFromDomainModel(obj,databaseFactory);
            
            this.dbContext.Database.EnsureCreated();
            this.Table.Add(entity);
            this.dbContext.SaveChangesAsync();
            entity.ConvertToDomainModel(obj,databaseFactory);
        }

        public List<DomainModel> GetAll()
        {
            List<DomainModel> domainObjs = new List<DomainModel>();

            this.dbContext.Database.EnsureCreated();
            Task<List<EntityType>> lst = this.Table.ToListAsync();
            lst.Wait(); // TODO: beter async uitwerken?
            foreach(EntityType entity in lst.Result)
            {
                DomainModel newDomainObj = new DomainModel();
                entity.ConvertToDomainModel(newDomainObj,databaseFactory);
                domainObjs.Add(newDomainObj);
            }

            return domainObjs;
        }

        public DomainModel Get(int id)
        {
            DomainModel model = new DomainModel();
            this.dbContext.Database.EnsureCreated();
            this.Table.Find(id).ConvertToDomainModel(model,databaseFactory);
           
            return model;
        }

        public void Modify(DomainModel obj)
        {
            EntityType entity = this.Table.Find(obj.Id);

            entity.ConvertFromDomainModel(obj,databaseFactory);

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

