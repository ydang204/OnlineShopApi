using OnlineShop.Common.Models;
using System.Collections.Generic;

namespace OnlineShop.UserService.Models
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}