using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.NotificationAPI.ServiceInterfaces;

namespace OnlineShop.NotificationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IMailService _emailSender;

        public TestController(IMailService emailsender)
        {
            _emailSender = emailsender;
        }

        [HttpPost]
        public async Task TestAction()
        {
            await _emailSender.SendEmailAsync("luongmanh67864@gmail.com", "subject", $"Enter email body here");
        }
    }
}