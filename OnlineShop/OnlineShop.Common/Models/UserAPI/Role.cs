using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Common.Models.UserAPI
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
