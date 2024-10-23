using Microsoft.EntityFrameworkCore;
using WebApp_vSem3.Models;

namespace WebApp_vSem3.Data
{
    public class WebAppContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        public WebAppContext() { }

        public WebAppContext(DbContextOptions<WebAppContext> dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id)
                      .HasName("product_pk");

                entity.Property(p => p.Name)
                      .HasMaxLength(255);
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.ProductGroup)
                      .WithMany(p => p.Products)
                      .HasForeignKey(p => p.ProductGroupId);
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pq => pq.Id)
                      .HasName("product_group_pk");

                entity.ToTable("Category");

                entity.Property(pq => pq.Name)
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.Id)
                      .HasName("storage_pk");

                entity.Property(s => s.Quantity).HasColumnName("Quantity");

                entity.HasOne(s => s.Product)
                      .WithMany(p => p.Storages)
                      .HasForeignKey(p => p.ProductId);
            });
        }
    }
}
