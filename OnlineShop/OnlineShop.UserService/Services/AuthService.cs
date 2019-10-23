using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Common.Exceptions;
using OnlineShop.Common.SettingOptions;
using OnlineShop.UserService.Models;
using OnlineShop.UserService.Models.ReqModels;
using OnlineShop.UserService.Models.ResModel;
using OnlineShop.UserService.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.UserService.Services
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
                //new Claim(AuthConstants.ACCOUNT_ID_CLAIM_TYPE, model.accountId.ToString()),
                //new Claim(AuthConstants.ACCOUNT_USERNAME_CLAIM_TYPE, model.userName),
                //new Claim(AuthConstants.EMPLOYEE_FULLNAME_CLAIM_TYPE, model.fullName),
                //new Claim(AuthConstants.EMPLOYEE_ID_CLAIM_TYPE, model.employeeId.ToString()),
                //new Claim(AuthConstants.EMPLOYER_ID_CLAIM_TYPE, model.employerId.ToString()),
                //new Claim(AuthConstants.EMPLOYEE_DEPARTMENT_CLAIM_TYPE, model.departmentId.ToString()),
                //new Claim(AuthConstants.EMPLOYEE_BRANCH_CLAIM_TYPE, model.branchId.ToString()),
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
                throw new CustomException("11001", "Username or password do not correct");
            }

            var token = GenerateToken(account);

            return new LoginResModel()
            {
                Token = token
            };
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