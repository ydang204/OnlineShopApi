namespace OnlineShop.Common.Models.UserAPI.ResModels
{
    public class LoginResModel
    {
        public string Token { get; set; }

        public AccountResModel Account { get; set; }
    }
}