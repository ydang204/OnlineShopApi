using OnlineShop.Common.Models;
using System.Collections.Generic;

namespace OnlineShop.UserService.Models
{
    public class Permission : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}