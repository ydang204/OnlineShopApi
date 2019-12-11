using System.Collections.Generic;

namespace OnlineShop.Common.Models.OrderAPI.ReqModels.Orders
{
    public class CreateOrderReqModel
    {
        public string  Receiver { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        
        public string Note { get; set; }

        public decimal Total { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public List<CreateOrderProductReqModel> Products { get; set; }
    }
}