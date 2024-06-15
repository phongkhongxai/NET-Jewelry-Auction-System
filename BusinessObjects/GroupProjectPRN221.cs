using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BusinessObjects
{
    public class GroupProjectPRN221 : DbContext
    {
        public GroupProjectPRN221()
        {
        }

        public GroupProjectPRN221(DbContextOptions<GroupProjectPRN221> options)
            : base(options)
        {
        }

        // DbSet cho các lớp mô hình
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<JewelryMaterial> JewelryMaterials { get; set; }
        public DbSet<UserAuction> UserAuctions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlServer(GetConnectionString());
                //optionsBuilder.UseSqlServer("Server=(local);database=GroupProjectPRN221;uid=sa;pwd=12345;TrustServerCertificate=True");

            }
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DefaultConnectionString"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100); 
                entity.Property(e => e.Phone).HasMaxLength(15);
                entity.Property(e => e.Dob).HasColumnType("date");
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.Password).IsRequired();
                entity.HasOne(e => e.Role).WithMany(r => r.User).HasForeignKey(e => e.RoleId); 
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Phone).IsUnique();

            });

            // Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Auction
            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("Auctions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
                entity.HasMany(e => e.Bids).WithOne(b => b.Auction).HasForeignKey(b => b.AuctionId);
                entity.HasMany(e => e.Invoices).WithOne(i => i.Auction).HasForeignKey(i => i.AuctionId);
            });

            // Jewelry
            modelBuilder.Entity<Jewelry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.Price).IsRequired();
                entity.HasMany(e => e.Auctions).WithOne(a => a.Jewelry).HasForeignKey(a => a.JewelryId);
            });

            // Material
            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            // Bid
            modelBuilder.Entity<Bid>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.BidTime).IsRequired().HasColumnType("datetime");
                entity.HasOne(e => e.User).WithMany(u => u.Bids).HasForeignKey(e => e.UserId);
            });

            // Invoice
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalPrice).IsRequired();
                entity.Property(e => e.InvoiceDate).IsRequired().HasColumnType("datetime");
                entity.HasOne(e => e.User).WithMany(u => u.Invoices).HasForeignKey(e => e.UserId);
            });

            // JewelryMaterial
            modelBuilder.Entity<JewelryMaterial>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Jewelry).WithMany(j => j.JewelryMaterials).HasForeignKey(e => e.JewelryId);
                entity.HasOne(e => e.Material).WithMany(m => m.JewelryMaterials).HasForeignKey(e => e.MaterialId);
            });

            // UserAuction
            modelBuilder.Entity<UserAuction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User).WithMany(u => u.UserAuctions).HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Auction).WithMany(a => a.UserAuctions).HasForeignKey(e => e.AuctionId);
            });
        }
    }
}
