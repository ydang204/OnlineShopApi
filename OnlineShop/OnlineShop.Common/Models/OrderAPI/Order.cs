using System.Collections.Generic;

namespace OnlineShop.Common.Models.OrderAPI
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Canclled
    }

    public class Order : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int ToTal { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}