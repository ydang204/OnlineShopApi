using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.OrderAPI;
using OnlineShop.Common.Models.OrderAPI.ReqModels.Orders;
using OnlineShop.Common.Models.OrderAPI.ResModels;

namespace OnlineShop.OrderAPI.ServiceInterfaces
{
    public interface IOrderService
    {
        Task<CreateOrderResModel> CreateOrderAsync(Order order);
        Task<BasePagingResponse<OrderDetailsResModel>> GetOrdersAsync(GetOrderReqModel model);
    }
}
