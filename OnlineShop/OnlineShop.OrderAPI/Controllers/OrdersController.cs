using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;

namespace OnlineShop.OrderAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        public OrdersController()
        {

        }
    }
}