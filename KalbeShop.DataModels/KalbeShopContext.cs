using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace KalbeShop.DataModels
{
    public partial class KalbeShopContext : DbContext
    {
        public KalbeShopContext()
        {
        }

        public KalbeShopContext(DbContextOptions<KalbeShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Penjualan> Penjualan { get; set; }
        public virtual DbSet<Produk> Produk { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=KalbeShop;user id=sa;Password=P@ssw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IntCustomerId);

                entity.Property(e => e.IntCustomerId).HasColumnName("intCustomerID");

                entity.Property(e => e.BitGender).HasColumnName("bitGender");

                entity.Property(e => e.DtInserted)
                    .HasColumnName("dtInserted")
                    .HasColumnType("datetime");

                entity.Property(e => e.DtmBirthDate)
                    .HasColumnName("dtmBirthDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TxtCustomerAddress)
                    .HasColumnName("txtCustomerAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TxtCustomerName)
                    .HasColumnName("txtCustomerName")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Penjualan>(entity =>
            {
                entity.HasKey(e => e.IntSalesOrderId);

                entity.Property(e => e.IntSalesOrderId).HasColumnName("intSalesOrderID");

                entity.Property(e => e.DtSalesOrder)
                    .HasColumnName("dtSalesOrder")
                    .HasColumnType("datetime");

                entity.Property(e => e.IntCustomerId).HasColumnName("intCustomerID");

                entity.Property(e => e.IntProductId).HasColumnName("intProductID");

                entity.Property(e => e.IntQty)
                    .HasColumnName("intQty")
                    .HasColumnType("decimal(10, 0)");
            });

            modelBuilder.Entity<Produk>(entity =>
            {
                entity.HasKey(e => e.IntProductId);

                entity.Property(e => e.IntProductId).HasColumnName("intProductID");

                entity.Property(e => e.DecPrice)
                    .HasColumnName("decPrice")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DtInserted)
                    .HasColumnName("dtInserted")
                    .HasColumnType("datetime");

                entity.Property(e => e.IntQty).HasColumnName("intQty");

                entity.Property(e => e.TxtProductCode)
                    .IsRequired()
                    .HasColumnName("txtProductCode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TxtProductName)
                    .HasColumnName("txtProductName")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
