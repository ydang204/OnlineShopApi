namespace OnlineShop.Common.Models.UserAPI.ReqModels
{
    public class ExternalLoginReqModel
    {
        public string FullName { get; set; }

        public string ExternalId { get; set; }

        public string Email { get; set; }
    }
}