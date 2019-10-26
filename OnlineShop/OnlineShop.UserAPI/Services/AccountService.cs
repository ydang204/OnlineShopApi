using OnlineShop.UserAPI.Models;
using OnlineShop.UserAPI.ServiceInterfaces;

namespace OnlineShop.UserAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserContext _context;

        public AccountService(UserContext context)
        {
            _context = context;
        }
    }
}