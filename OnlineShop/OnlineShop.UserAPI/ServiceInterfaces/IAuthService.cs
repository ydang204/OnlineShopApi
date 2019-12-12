using OnlineShop.Common.Models.UserAPI;
using OnlineShop.Common.Models.UserAPI.ReqModels;
using OnlineShop.Common.Models.UserAPI.ResModels;

using System.Threading.Tasks;

namespace OnlineShop.UserAPI.ServiceInterfaces
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


        Task<LoginResModel> ExternalLoginAsync(ExternalLoginReqModel model);
    }
}