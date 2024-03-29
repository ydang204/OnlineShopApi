﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.OrderAPI;
using OnlineShop.Common.Models.OrderAPI.ReqModels.Orders;
using OnlineShop.Common.Models.OrderAPI.ResModels;
using OnlineShop.OrderAPI.ServiceInterfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.OrderAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<CreateOrderResModel> CreateOrder([FromBody] CreateOrderReqModel model)
        {
            var order = _mapper.Map<CreateOrderReqModel, Order>(model);
            order.UserId = User.GetAccountId().Value;
            return await _orderService.CreateOrderAsync(order);
        }

        [HttpGet]
        public async Task<BasePagingResponse<OrderDetailsResModel>> GetOrder([FromQuery] GetOrderReqModel model) 
        {
            return await _orderService.GetOrdersAsync(model);
        }
    }
}