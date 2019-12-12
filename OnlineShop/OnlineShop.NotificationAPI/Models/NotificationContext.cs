using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.Notification;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Core;

namespace OnlineShop.NotificationAPI.Models
{
    public class NotificationContext : TrackerContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public NotificationContext(DbContextOptions<NotificationContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContext = httpContextAccessor;
        }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Device> Devices { get; set; }

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