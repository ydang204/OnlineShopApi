using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PingController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public string Ping()
        {
            return $"Product API is running on version {_hostingEnvironment.EnvironmentName}";
        }
    }
}