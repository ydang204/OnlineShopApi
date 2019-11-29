using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.UserAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PingController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// Use this API to check the service is running or not
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Ping()
        {
            return $"User API is running on version {_hostingEnvironment.EnvironmentName}";
        }
    }
}