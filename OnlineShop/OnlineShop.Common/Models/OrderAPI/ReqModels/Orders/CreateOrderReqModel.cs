using System.Collections.Generic;

namespace OnlineShop.Common.Models.OrderAPI.ReqModels.Orders
{
    public class CreateOrderReqModel
    {
        public List<CreateOrderProductReqModel> Products { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}