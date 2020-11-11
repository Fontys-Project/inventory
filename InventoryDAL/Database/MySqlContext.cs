using Microsoft.EntityFrameworkCore;
using InventoryDAL.Tags;
using InventoryDAL.ProductTag;
using InventoryDAL.Products;
using InventoryDAL.Stocks;

namespace InventoryDAL.Database
{
    public class MySqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use docker composer mysql credentials (safe to store in code)
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseMySQL(
                "server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Sku).IsRequired();
                entity.HasMany(e => e.StockEntities)
                      .WithOne(s => s.ProductEntity)
                      .HasForeignKey(s => s.ProductId);
                entity.HasMany(e => e.ProductTagEntities)
                      .WithOne(p => p.ProductEntity)
                      .HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<StockEntity>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Amount).IsRequired();
                entity.Property(s => s.Date).IsRequired();
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<ProductTagEntity>(entity =>
            {
                entity.HasKey(t => new { t.ProductId, t.TagId });

                entity.HasOne(pt => pt.ProductEntity)
                    .WithMany(p => p.ProductTagEntities)
                    .HasForeignKey(pt => pt.ProductId);

                entity.HasOne(pt => pt.TagEntity);
            });
                
        }
    }
}
