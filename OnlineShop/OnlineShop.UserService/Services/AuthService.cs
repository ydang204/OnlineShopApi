using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Common;
using OnlineShop.UserService.Models;
using OnlineShop.UserService.Models.ReqModels;
using OnlineShop.UserService.ServiceInterfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnlineShop.UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public AuthService(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoginReqModel> LoginAsync(LoginReqModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a=> a.UserName.ToLower() == model.UserName.ToLower());

            if (account == null)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "Username or password do not correct");
            }


            throw new NotImplementedException();
            
        }


        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RegisterAsync(RegisterReqModel model)
        {
            var account = _mapper.Map<RegisterReqModel, Account>(model);

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}