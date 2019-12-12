using OnlineShop.Common.Models.Common.ReqModels;

namespace OnlineShop.Common.Models.OrderAPI.ReqModels.Orders
{
    public class GetOrderReqModel : BasePagingRequest
    {
        public OrderStatus? Status { get; set; }
    }
}