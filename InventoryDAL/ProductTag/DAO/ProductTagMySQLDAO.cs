using InventoryDAL.Database;
using InventoryDAL.Interfaces;
using System.Linq;

namespace InventoryDAL.ProductTag
{
    public class ProductTagMySQLDAO : MySqlDAO<ProductTagEntity>, IProductTagDAO
    {
        public ProductTagMySQLDAO(MySqlContext context)
            : base(context)
        {
        }

        //protected readonly MySqlContext dbContext;
        //protected DbSet<InventoryLogic.ProductTag.ProductTag> Table { get; set; }

        //private IDAOFactory databaseFactory;

        //public ProductTagMySQLDAO(MySqlContext context, IDAOFactory databaseFactory)
        //{
        //    this.dbContext = context;
        //    this.Table = context.Set<InventoryLogic.ProductTag.ProductTag>();
        //    this.databaseFactory = databaseFactory;
        //}

        //public List<InventoryLogic.ProductTag.ProductTag> GetAll()
        //{
        //    this.dbContext.Database.EnsureCreated();
        //    Task<List<InventoryLogic.ProductTag.ProductTag>> lst = this.Table.ToListAsync();
        //    lst.Wait(); // TODO: beter async uitwerken?
        //    return lst.Result;
        //}

        public ProductTagEntity Get(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            return this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault();
        }

        //public void Add(InventoryLogic.ProductTag.ProductTag obj)
        //{
        //    this.dbContext.Database.EnsureCreated();
        //    this.Table.Add(obj);
        //    this.dbContext.SaveChangesAsync();
        //}

        //public void Modify(InventoryLogic.ProductTag.ProductTag obj)
        //{
        //    this.dbContext.Database.EnsureCreated();
        //    this.dbContext.Update(obj);
        //    this.dbContext.SaveChangesAsync();
        //}

        public void Remove(int productId, int tagId)
        {
            this.dbContext.Database.EnsureCreated();
            this.Table.Remove(
                this.Table.Where(j => j.ProductId == productId && j.TagId == tagId).FirstOrDefault()
                );
            this.dbContext.SaveChangesAsync();
        }
    }
}
