using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Inventory)
                .WithOne(pi => pi.Product)
                .HasForeignKey<Product_Inventory>(pi => pi.ProductId);

            modelBuilder.Entity<Discount>()
                .Property(d => d.Discount_Percent)
                .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Rate>()
              .Property(r => r.Value)
              .IsRequired()
              .HasPrecision(3, 1);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Address> User_Addresses { get; set; }
        public DbSet<User_Payment> User_Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Category> Product_Categories { get; set; }
        public DbSet<Product_Inventory> Product_Inventories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Order_Detail> Order_Details { get; set; }
        public DbSet<Payment_Details> Payment_Details { get; set; }
        public DbSet<Rate> Rates { get; set; }







    }
}
