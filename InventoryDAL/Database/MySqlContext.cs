using Microsoft.EntityFrameworkCore;
using InventoryLogic.Products;
using InventoryLogic.Tags;
using InventoryLogic.Stocks;
using InventoryLogic.ProductTagJoins;

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
                entity.HasMany(e => e.Stocks)
                      .WithOne()
                      .HasForeignKey(s => s.ProductId);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Amount).IsRequired();
                entity.Property(s => s.Date).IsRequired();
                //entity.HasOne(s => s.Product)
                //      .WithMany()
                //      .HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<ProductTagJoin>(entity =>
            {
              entity.HasKey(j => new { j.ProductId, j.TagId });
              entity.HasOne(j => j.Product)
                    .WithMany(p => p.ProductTagJoins)
                    .HasForeignKey(j => j.ProductId);
              entity.HasOne(j => j.Tag)
                    .WithMany(t => t.ProductTagJoins)
                    .HasForeignKey(j => j.TagId);
            });
        }
    }
}
