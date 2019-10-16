using OnlineShop.UserService.Models;
using OnlineShop.UserService.Models.ReqModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UserService.ServiceInterfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterReqModel model);

        Task<LoginReqModel> Login(LoginReqModel model);
    }
}
