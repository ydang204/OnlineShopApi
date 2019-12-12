using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.OrderAPI;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Core;

namespace OnlineShop.OrderAPI.Models
{
    public class OrderContext : TrackerContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public OrderContext(DbContextOptions<OrderContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContext = httpContextAccessor;
        }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }



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