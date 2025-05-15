using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Data
{
    public class ProductDbContext : DbContext
    {

        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options)
        {
             Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 1,
                Name = "Decade",
                Price = 10.0m,
                Quantity = 100,
            });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 2,
                Name = "Double",
                Price = 20.0m,
                Quantity = 100,
            });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 3,
                Name = "OOO",
                Price = 30.0m,
                Quantity = 100,
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ProductModel> Products { get; set; }
    }
}
