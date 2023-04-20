using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<PurchaseOrderEntity> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItemEntity> PurchaseOrderItems { get; set; }
        public DbSet<SalesOrderEntity> SalesOrders { get; set; }
        public DbSet<SalesOrderItemEntity> SalesOrderItems { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<InventoryItemEntity> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasDiscriminator<string>("UserType")
                .HasValue<AdminEntity>("Admin")
                .HasValue<RetailerEntity>("Retailer");
            modelBuilder.Entity<SalesOrderEntity>()
                .HasOne(so => so.Customer)
                .WithMany(c => c.SalesOrders)
                .HasForeignKey(so => so.CusomterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrderEntity>()
                .HasOne(so => so.Retailer)
                .WithMany(r => r.SalesOrders)
                .HasForeignKey(so => so.RetailerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrderEntity>()
                .HasOne(so => so.Location)
                .WithMany(l => l.ListOfSalesOrders)
                .HasForeignKey(so => so.LocationId)
                .OnDelete(DeleteBehavior.Restrict); // Change delete behavior to Restrict

            modelBuilder.Entity<LocationEntity>()
                .HasOne(l => l.Retailer)
                .WithMany(r => r.Locations) // Assuming 'Locations' is the navigation property in the RetailerEntity
                .HasForeignKey(l => l.RetailerId)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}