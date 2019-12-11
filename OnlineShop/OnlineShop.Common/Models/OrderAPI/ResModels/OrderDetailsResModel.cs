using System;

namespace OnlineShop.Common.Models.OrderAPI.ResModels
{
    public class OrderDetailsResModel
    {
        public int UserId { get; set; }

        public string Receiver { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Note { get; set; }

        public decimal ToTal { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}