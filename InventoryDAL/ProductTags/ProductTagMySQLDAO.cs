using InventoryLogic.Product;
using System;
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
            optionsBuilder.UseMySQL("server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Sku).IsRequired();
            });
        }

        public void AddTag(ProductTag tag)
        {
            this.Database.EnsureCreated();
            this.ProductTags.Add(tag);
            this.SaveChangesAsync();
        }

        public List<ProductTag> GetAllTags()
        {
            this.Database.EnsureCreated();
            Task<List<ProductTag>> tags = ProductTags.ToListAsync();

            tags.Wait();

            return tags.Result;
        }

        public ProductTag GetTag(int id)
        {
            this.Database.EnsureCreated();
            return this.ProductTags.Find(id);
        }

        public void ModifyTag(ProductTag tag)
        {
            this.Database.EnsureCreated();
            this.ProductTags.Update(tag);
            this.SaveChangesAsync();
        }

        public void RemoveTag(int id)
        {
            this.Database.EnsureCreated();
            this.ProductTags.Remove(this.ProductTags.Find(id));
            this.SaveChangesAsync();
        }
    }
}
