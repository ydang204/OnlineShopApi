using OnlineShop.Common.Constants;
using System.Security.Claims;

namespace OnlineShop.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetAccountId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            if (user.Claims == null)
            {
                return null;
            }

            var claim = user.FindFirst(AuthConstants.ACCOUNT_ID_CLAIM_TYPE);
            if (claim == null) return null;

            if (int.TryParse(claim.Value, out int id))
            {
                return id;
            }
            else
            {
                return null;
            }
        }

        public static string GetFullName(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            if (user.Claims == null)
            {
                return null;
            }

            var claim = user.FindFirst(AuthConstants.EMPLOYEE_FULLNAME_CLAIM_TYPE);
            return claim != null ? claim.Value : null;
        }
    }
}