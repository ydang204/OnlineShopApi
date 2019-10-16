using OnlineShop.Common.Models;

namespace OnlineShop.UserService.Model
{
    public class Account : BaseEntity<int>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}