using Microsoft.EntityFrameworkCore;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;

namespace InventoryDAL.Database
{
    public class MySqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use docker composer mysql credentials (safe to store in code)
            optionsBuilder.UseMySQL(
                "server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner;");
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
                //entity.HasMany(e => e.Stocks).WithOne().HasForeignKey(s => s.ProductId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.HasIndex(t => t.Name).IsUnique();
                entity.HasMany(t => t.Products)
                        .WithOne(p => p.Tag);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Amount).IsRequired();
                entity.Property(s => s.Date).IsRequired();
                entity.HasOne(s => s.Product)
                        .WithMany()
                        .HasForeignKey(p => p.ProductId);
            });
        }
    }
}
