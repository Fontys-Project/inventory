using InventoryLogic.Product;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventoryDAL.Product
{
    public class ProductMySQLDAO : DbContext, IProductDAO
    {

        private DbSet<InventoryLogic.Product.Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("CONNECTSTRING"));
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryLogic.Product.Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }


        public void AddProduct(InventoryLogic.Product.Product product)
        {
            this.Product.Add(product);
            this.SaveChangesAsync();
        }

        public List<InventoryLogic.Product.Product> GetAllProducts()
        {
            this.Database.EnsureCreated();

            Task<List<InventoryLogic.Product.Product>> products = Product.ToListAsync();

            products.Wait();

            return products.Result;
        }

        public InventoryLogic.Product.Product GetProduct(int ID)
        {
            throw new NotImplementedException();
        }

        public void ModifyProduct(InventoryLogic.Product.Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
