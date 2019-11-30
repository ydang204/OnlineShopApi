using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Models.UserAPI.ReqModels;
using OnlineShop.Common.Models.UserAPI.ResModels;
using OnlineShop.UserAPI.ServiceInterfaces;
using System.Threading.Tasks;

namespace OnlineShop.UserAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Allow user register new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterReqModel model)
        {
            await _authService.RegisterAsync(model);
            return Ok(new { registerSucceed = true });
        }

        [HttpPost("login")]
        public async Task<LoginResModel> Login(LoginReqModel model)
        {
            return await _authService.LoginAsync(model);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordReqModel model)
        {
            await _authService.ForgotPasswordAsync(model);
            return Ok(new { canResetPassword = true });
        }
    }
}