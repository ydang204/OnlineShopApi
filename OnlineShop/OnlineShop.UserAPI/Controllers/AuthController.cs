using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Models.UserAPI.ReqModels;
using OnlineShop.UserAPI.ServiceInterfaces;
using System.Threading.Tasks;

namespace OnlineShop.UserAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterReqModel model)
        {
            await _authService.RegisterAsync(model);
            return Ok(new { registerSucceed = true });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqModel model)
        {
            var response = await _authService.LoginAsync(model);

            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordReqModel model)
        {
            await _authService.ForgotPasswordAsync(model);
            return Ok(new { canResetPassword = true });
        }
    }
}