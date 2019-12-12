using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Exceptions;
using OnlineShop.Common.Models.UserAPI;
using OnlineShop.Common.Models.UserAPI.ReqModels;
using OnlineShop.Common.Models.UserAPI.ResModels;
using OnlineShop.Common.Resources;
using OnlineShop.Common.SettingOptions;
using OnlineShop.UserAPI.Models;
using OnlineShop.UserAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.UserAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        private readonly JwtTokenOptions _tokenOptions;

        public AuthService(UserContext context, IMapper mapper, IOptions<JwtTokenOptions> tokenOptions)
        {
            _context = context;
            _mapper = mapper;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<LoginResModel> ExternalLoginAsync(ExternalLoginReqModel model)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == model.Email);

            if (user == null)
            {
                var registerModel = new RegisterReqModel
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.ExternalId,
                    FullName = model.FullName
                };
                await RegisterAsync(registerModel);

            }

            var loginModel = new LoginReqModel()
            {
                Password = model.ExternalId,
                UserName = model.Email
            };
            return await LoginAsync(loginModel);
        }

        public Task ForgotPasswordAsync(ForgotPasswordReqModel model)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(Account account)
        {
            // TODO: Complete jwt token generator

            var issuedTime = DateTime.UtcNow;
            var expiredAt = issuedTime.Add(_tokenOptions.Expiration);
            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, (issuedTime.Subtract(new DateTime(1970,1,1))).TotalSeconds.ToString(), ClaimValueTypes.Integer64),
                new Claim(AuthConstants.ACCOUNT_ID_CLAIM_TYPE, account.Id.ToString()),
                new Claim(AuthConstants.ACCOUNT_USERNAME_CLAIM_TYPE, account.UserName),
                new Claim(AuthConstants.EMPLOYEE_FULLNAME_CLAIM_TYPE, account.FullName ?? "")
            };
            //.Concat(permissions.Select(p => new Claim(ClaimTypes.Role, p)));

            // Create the JWT and write it to a string

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                notBefore: issuedTime,
                expires: expiredAt,
                signingCredentials: _tokenOptions.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<LoginResModel> LoginAsync(LoginReqModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName.ToLower() == model.UserName.ToLower());

            if (account == null)
            {
                throw new CustomException(Errors.USERNAME_PASSWORD_DO_NOT_CORRECT, Errors.USERNAME_PASSWORD_DO_NOT_CORRECT_MSG);
            }

            var token = GenerateToken(account);

            return new LoginResModel()
            {
                Token = token,
                Account = _mapper.Map<Account, AccountResModel>(account)
            };
        }

        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RegisterAsync(RegisterReqModel model)
        {
            var existAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName.ToLower().Equals(model.UserName.ToLower()) || a.Email.ToLower().Equals(model.Email.ToLower()));

            if (existAccount != null)
            {
                throw new CustomException(Errors.ACCOUNT_HAS_REGISTERED, Errors.ACCOUNT_HAS_REGISTERED_MSG);
            }

            var account = _mapper.Map<RegisterReqModel, Account>(model);

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}