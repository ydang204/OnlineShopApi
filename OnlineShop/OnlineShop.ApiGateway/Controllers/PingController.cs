using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.ApiGateway.Controllers
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
            return $"API Gateway is running on version {_hostingEnvironment.EnvironmentName}";
        }
    }
}