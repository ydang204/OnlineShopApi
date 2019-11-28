using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Models.OrderAPI;

namespace OnlineShop.OrderAPI.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        #region Db Sets

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        #endregion Db Sets
    }
}