using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.OrderAPI;
using OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment;
using OnlineShop.Common.Models.OrderAPI.ReqModels.Orders;
using OnlineShop.Common.Models.OrderAPI.ResModels;
using OnlineShop.Common.Utitlities;
using OnlineShop.OrderAPI.Models;
using OnlineShop.OrderAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<BasePagingResponse<OrderDetailsResModel>> GetOrdersAsync(GetOrderReqModel model)
        {
            var result = new BasePagingResponse<OrderDetailsResModel>();
            var query = _context.Orders.AsNoTracking();

            if (model.Status.HasValue)
            {
                query = query.Where(o => o.Status == model.Status);
            }

            query = CommonFunctions.SortQuery(model, query);

            result.Total = await query.CountAsync();
            if (model.Page.HasValue && model.Page >= 0 && model.PageSize.HasValue && model.PageSize > 0)
            {
                query = query.Skip(model.PageSize.Value * (model.Page.Value - 1)).Take(model.PageSize.Value);
            }

            var orders = await query.ToListAsync();

            result.Items = _mapper.Map<List<Order>, List<OrderDetailsResModel>>(orders);
            result.Page = model.Page;
            result.PageSize = model.PageSize;
            result.TotalPage = (int)Math.Ceiling(result.Total / (double)result.PageSize);

            return result;
        }
    }
}