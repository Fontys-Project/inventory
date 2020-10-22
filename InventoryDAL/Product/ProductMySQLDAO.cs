using InventoryLogic.Product;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventoryDAL.Product
{
    public class ProductMySQLDAO : DbContext, IProductDAO
    {
        private DbSet<InventoryLogic.Product.Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use docker composer mysql credentials (safe to store in code)
            optionsBuilder.UseMySQL("server=db;port=3306;userid=dbuser;password=dbuserpassword;database=accountowner;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryLogic.Product.Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Sku).IsRequired();
            });
        }

        public void AddProduct(InventoryLogic.Product.Product product)
        {
            this.Database.EnsureCreated();
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
            this.Database.EnsureCreated();
            return this.Product.Find(ID);
        }

        public void ModifyProduct(InventoryLogic.Product.Product product, int id)
        {
            this.Database.EnsureCreated();
            InventoryLogic.Product.Product curProduct = this.GetProduct(id);
            curProduct.Name = product.Name;
            curProduct.Price = product.Price;
            curProduct.Sku = product.Sku;
            this.Product.Update(curProduct);
            this.SaveChangesAsync();
        }

        public void RemoveProduct(int ID)
        {
            this.Database.EnsureCreated();
            this.Product.Remove(this.Product.Find(ID));
            this.SaveChangesAsync();
        }
    }
}
