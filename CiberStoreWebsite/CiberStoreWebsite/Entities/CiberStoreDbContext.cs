using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CiberStoreWebsite.Entities
{
    public class CiberStoreDbContext : IdentityDbContext<IdentityUser>
    {
        public CiberStoreDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder
            .Entity<Order>()
            .HasOne(e => e.Product)
            .WithMany(e => e.Orders)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
            .Entity<Order>()
            .HasOne(e => e.Customer)
            .WithMany(e => e.Orders)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
            .Entity<Product>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Name = "Visitor", NormalizedName = "VISITOR" },
               new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });

            modelBuilder.Entity<Customer>().HasData(
                new Customer() { Id = 1, Name = "Ronaldo", Address = "Manchester" },
                new Customer() { Id = 2, Name = "Messi", Address = "Paris" },
                new Customer() { Id = 3, Name = "Neymar", Address = "Monaco" });

            modelBuilder.Entity<Category>().HasData(
               new Category() { Id = 1, Name = "Clothes", Description = "Clothes" },
               new Category() { Id = 2, Name = "Houseware", Description = "Houseware" },
               new Category() { Id = 3, Name = "Furniture", Description = "Furniture" });

            modelBuilder.Entity<Product>().HasData(
               new Product() { Id = 1, Name = "Jacket", CategoryId = 1, Description = "Black Jacket", Price = 300, Quantity = 505 },
               new Product() { Id = 2, Name = "Jeans", CategoryId = 1, Description = "Gray Jeans", Price = 500, Quantity = 440 },
               new Product() { Id = 3, Name = "Television", CategoryId = 2, Description = "Samsung smart TV", Price = 1000, Quantity = 600 },
               new Product() { Id = 4, Name = "Fridge", CategoryId = 2, Description = "LG Fridge", Price = 2500, Quantity = 326 },
               new Product() { Id = 5, Name = "Sofa", CategoryId = 3, Description = "Sofa", Price = 8000, Quantity = 200 });

            modelBuilder.Entity<Order>().HasData(
              new Order() { Id = 1, CustomerId = 1, ProductId = 1, Amount = 3, OrderDate = new DateTime(2022, 3, 30) },
              new Order() { Id = 2, CustomerId = 1, ProductId = 2, Amount = 6, OrderDate = DateTime.Now },
              new Order() { Id = 3, CustomerId = 2, ProductId = 2, Amount = 1, OrderDate = DateTime.Now },
              new Order() { Id = 4, CustomerId = 2, ProductId = 3, Amount = 8, OrderDate = DateTime.Now },
              new Order() { Id = 5, CustomerId = 3, ProductId = 3, Amount = 12, OrderDate = DateTime.Now },
              new Order() { Id = 6, CustomerId = 3, ProductId = 4, Amount = 2, OrderDate = new DateTime(2022, 3, 30) },
              new Order() { Id = 7, CustomerId = 1, ProductId = 4, Amount = 9, OrderDate = new DateTime(2022, 3, 30) },
              new Order() { Id = 8, CustomerId = 2, ProductId = 5, Amount = 2, OrderDate = new DateTime(2022, 3, 30) });
        }
    }
}
