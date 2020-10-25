using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryLogic.Products;
using InventoryLogic.ProductTags;
using InventoryLogic.Stocks;

namespace InventoryDAL.Database
{
    public class MySqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use docker composer mysql credentials (safe to store in code)
            optionsBuilder.UseMySQL("server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Sku).IsRequired();
                entity.HasOne(p => p.Tag)
                        .WithMany(t => t.Products)
                        .HasForeignKey(p => p.TagId);
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Sku).IsRequired();
                //entity.HasMany(e => e.Stocks).WithOne().HasForeignKey(s => s.ProductId);
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.HasMany(t => t.Products)
                        .WithOne(p => p.Tag);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Product).WithMany().HasForeignKey(p => p.ProductId);
                
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Today).IsRequired();
            });
        }
    }
}
