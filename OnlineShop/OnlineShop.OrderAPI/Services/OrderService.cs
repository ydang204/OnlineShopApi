using AutoMapper;
using OnlineShop.Common.Models.OrderAPI;
using OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment;
using OnlineShop.Common.Models.OrderAPI.ReqModels.Orders;
using OnlineShop.Common.Models.OrderAPI.ResModels;
using OnlineShop.Common.Utitlities;
using OnlineShop.OrderAPI.Models;
using OnlineShop.OrderAPI.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;
        private readonly MoMoPaymentHelper _moMoPaymentHelper;
        private readonly IMapper _mapper;

        public OrderService(OrderContext context, MoMoPaymentHelper moMoPaymentHelper, IMapper mapper)
        {
            _context = context;
            _moMoPaymentHelper = moMoPaymentHelper;
            _mapper = mapper;
        }

        public async Task<CreateOrderResModel> CreateOrderAsync(Order order)
        {
            var result = new CreateOrderResModel();
            if (order.PaymentMethod == PaymentMethod.COD)
            {
                result.Status = OrderStatus.Pending;
                order.Status = OrderStatus.Pending;
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                order.Status = OrderStatus.WaitingPayment;
                result.Status = OrderStatus.WaitingPayment;
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                order.ToTal = 100000;
                var paymentModel = _mapper.Map<Order, PaymentReqModel>(order);

                result.PaymentUrl = await _moMoPaymentHelper.CreatePaymentRequestAync(paymentModel);
            }

            return result;
        }

        public Task<List<OrderDetailsResModel>> GetOrdersAsync(GetOrderReqModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}