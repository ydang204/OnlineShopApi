using System.Collections.Generic;

namespace OnlineShop.Common.Models.UserAPI
{
    public class Permission : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}