using SalesApp.Model;
using Microsoft.EntityFrameworkCore;

namespace SalesApp.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Categories)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(a => a.product)
                .WithMany(s => s.SalesProducts)
                .HasForeignKey(a => a.product_id);
        }
    }
}
