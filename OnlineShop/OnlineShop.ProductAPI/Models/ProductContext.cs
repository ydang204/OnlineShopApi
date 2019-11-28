using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Models.ProductAPI;

namespace OnlineShop.ProductAPI.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Brand> Brands { get; set; }
    }
}