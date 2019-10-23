using Microsoft.IdentityModel.Tokens;
using System;

namespace OnlineShop.Common.SettingOptions
{
    public class JwtTokenOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiresInDays { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30);

        public SigningCredentials SigningCredentials { get; set; }
    }
}