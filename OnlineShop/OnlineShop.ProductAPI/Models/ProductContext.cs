using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.ProductAPI;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Core;

namespace OnlineShop.ProductAPI.Models
{
    public class ProductContext : TrackerContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public ProductContext(DbContextOptions<ProductContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContext = httpContextAccessor;
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Brand> Brands { get; set; }


        public override Task<int> SaveChangesAsync()
        {
            UpdateTimeTracker();
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            UpdateTimeTracker();
            return base.SaveChanges();
        }

        private void UpdateTimeTracker()
        {
            var dbContextExtension = new DbContextExtensions(_httpContext, ChangeTracker);
            dbContextExtension.UpdateTimeTracker();
        }
    }
}