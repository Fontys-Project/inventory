using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryLogic.ProductTags;

namespace InventoryDAL.ProductTags
{
    public class ProductTagMySQLDAO : DbContext, IProductTagDAO
    {
        private DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use docker composer mysql credentials (safe to store in code)
            optionsBuilder.UseMySQL("server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner2;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }

        public void Add(ProductTag tag)
        {
            this.Database.EnsureCreated();
            this.ProductTags.Add(tag);
            this.SaveChangesAsync();
        }

        public List<ProductTag> GetAll()
        {
            this.Database.EnsureCreated();
            Task<List<ProductTag>> tags = ProductTags.ToListAsync();
            tags.Wait(); // TODO: beter async uitwerken?
            return tags.Result;
        }

        public ProductTag Get(int id)
        {
            this.Database.EnsureCreated();
            return this.ProductTags.Find(id);
        }

        public void Modify(ProductTag tag, int id)
        {
            this.Database.EnsureCreated();
            ProductTag curTag = this.Get(id);
            curTag.Name = tag.Name;
            this.ProductTags.Update(curTag);
            this.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            this.Database.EnsureCreated();
            this.ProductTags.Remove(this.ProductTags.Find(id));
            this.SaveChangesAsync();
        }
    }
}
