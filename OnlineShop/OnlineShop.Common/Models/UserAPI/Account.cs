using System;
using System.Collections.Generic;

namespace OnlineShop.Common.Models.UserAPI
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