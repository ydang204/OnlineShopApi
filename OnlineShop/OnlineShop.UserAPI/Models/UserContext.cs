using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.UserAPI;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Core;

namespace OnlineShop.UserAPI.Models
{
    public class UserContext : TrackerContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContext(DbContextOptions<UserContext> options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContext = httpContextAccessor;
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure PK for tables have 2 more keys
            modelBuilder.Entity<AccountRole>().HasKey(e => new { e.AccountId, e.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(e => new { e.PermissionId, e.RoleId });
            base.OnModelCreating(modelBuilder);
        }

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