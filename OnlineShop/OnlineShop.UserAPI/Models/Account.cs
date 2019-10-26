using OnlineShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UserAPI.Models
{
    public class Account : BaseEntity<Guid>
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
