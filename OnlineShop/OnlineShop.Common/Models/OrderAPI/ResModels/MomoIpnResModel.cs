using Newtonsoft.Json.Linq;

namespace OnlineShop.Common.Models.OrderAPI.ResModels
{
    public class MomoIpnResModel
    {
        public string PartnerCode { get; set; }

        public string AccessKey { get; set; }

        public string RequestId { get; set; }

        public string OrderId { get; set; }

        public int ErrorCode { get; set; }

        public string Message { get; set; }

        public string ResponseTime { get; set; }

        public JObject ExtraData { get; set; }

        public string Signature { get; set; }
    }
}