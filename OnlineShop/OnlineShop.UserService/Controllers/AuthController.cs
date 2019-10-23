using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.UserService.Models.ReqModels;
using OnlineShop.UserService.ServiceInterfaces;

namespace OnlineShop.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterReqModel model)
        {
            await _authService.RegisterAsync(model);
            return Ok(new { registerSucceed = true });
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginReqModel model)
        {
            var response = await _authService.LoginAsync(model);

            return Ok(response);
        }
    }
}