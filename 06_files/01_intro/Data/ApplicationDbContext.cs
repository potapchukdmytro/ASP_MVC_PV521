using _01_intro.Models;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

                e.Property(c => c.Description)
                .HasColumnType("text");

                e.Property(c => c.Image)
                .HasMaxLength(50);
            });

            // Product
            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(p => p.Id);

                e.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

                e.Property(p => p.Description)
                .HasColumnType("text");

                e.Property(p => p.Image)
                .HasMaxLength(255);
            });

            // Relationships

            // Product <-> Category many to one
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
