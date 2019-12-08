using OnlineShop.Common.Models.OrderAPI;
using OnlineShop.Common.Models.OrderAPI.ResModels;
using OnlineShop.Common.Utitlities;
using OnlineShop.OrderAPI.Models;
using OnlineShop.OrderAPI.ServiceInterfaces;
using System.Threading.Tasks;

namespace OnlineShop.OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;
        private readonly MoMoPaymentHelper _moMoPaymentHelper;

        public OrderService(OrderContext context, MoMoPaymentHelper moMoPaymentHelper)
        {
            _context = context;
            _moMoPaymentHelper = moMoPaymentHelper;
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
                result.PaymentUrl = await _moMoPaymentHelper.CreatePaymentRequestAync(order);
            }

            return result;
        }
    }
}