using Microsoft.EntityFrameworkCore;

namespace OnlineShop.UserAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
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
    }
}