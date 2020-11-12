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
                .UseMySQL("server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>(productEntity =>
            {
                productEntity.HasKey(p => p.Id);
                productEntity.Property(p => p.Name).IsRequired();
                productEntity.Property(p => p.Sku).IsRequired();
                productEntity.HasMany(e => e.StockEntities)
                      .WithOne(s => s.ProductEntity)
                      .HasForeignKey(s => s.ProductId);
                productEntity.HasMany(e => e.ProductTagEntities)
                     .WithOne(p => p.ProductEntity)
                     .HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<StockEntity>(stockEntity =>
            {
                stockEntity.HasKey(s => s.Id);
                stockEntity.Property(s => s.Amount).IsRequired();
                stockEntity.Property(s => s.Date).IsRequired();
            });

            modelBuilder.Entity<TagEntity>(tagEntity =>
            {
                tagEntity.HasKey(t => t.Id);
                tagEntity.Property(t => t.Name).IsRequired();
                //entity.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<ProductTagEntity>(productTag =>
            {
                productTag.HasKey(t => new { t.ProductId, t.TagId });
                productTag.HasOne(pt => pt.ProductEntity)
                          .WithMany(p => p.ProductTagEntities)
                          .HasForeignKey(pt => pt.ProductId);
                productTag.HasOne(pt => pt.TagEntity)
                          .WithMany(t => t.ProductTagEntities)
                          .HasForeignKey(pt => pt.TagId);
            });
        }
    }
}
