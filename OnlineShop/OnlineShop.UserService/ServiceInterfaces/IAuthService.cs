using OnlineShop.UserService.Models;
using OnlineShop.UserService.Models.ReqModels;
using OnlineShop.UserService.Models.ResModel;
using System.Threading.Tasks;

namespace OnlineShop.UserService.ServiceInterfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterReqModel model);

        Task<LoginResModel> LoginAsync(LoginReqModel model);

        string GenerateToken(Account account);

        /// <summary>
        /// Send email to user's email with the url allow user reset their password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ForgotPasswordAsync(ForgotPasswordReqModel model);
    }
}