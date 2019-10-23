using Microsoft.EntityFrameworkCore;

namespace OnlineShop.UserService.Models
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
            modelBuilder.Entity<AccountRole>().HasKey(e => new { e.AccountId, e.RoleId });
            base.OnModelCreating(modelBuilder);
        }
    }
}