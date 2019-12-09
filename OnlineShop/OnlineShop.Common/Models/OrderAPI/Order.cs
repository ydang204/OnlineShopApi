using System.Collections.Generic;

namespace OnlineShop.Common.Models.OrderAPI
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Canceled,
        Delivering,
        WaitingPayment,
        PaymentFailed
    }

    public enum PaymentMethod
    {
        COD,
        MoMo,
    }

    public class Order : BaseEntity<int>
    {
        public int UserId { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Note { get; set; }

        public decimal ToTal { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}