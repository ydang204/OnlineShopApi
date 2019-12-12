using OnlineShop.Common.SettingOptions;

namespace OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment
{
    public class MoMoPaymentReqModel
    {
        public string accessKey { get; set; }

        public string partnerCode { get; set; }

        public string requestType { get; set; }

        public string notifyUrl { get; set; }

        public string returnUrl { get; set; }

        public string orderId { get; set; }

        public string amount { get; set; }

        public string orderInfo { get; set; }

        public string requestId { get; set; }

        public string extraData { get; set; }

        public string signature { get; set; }

        public void SetMoMoData(MoMoPaymentOptions momoOptions, string signatureScripted)
        {
            signature = signatureScripted;
            accessKey = momoOptions.AccessKey;
            partnerCode = momoOptions.PartnerCode;
            requestType = momoOptions.RequestType;
            notifyUrl = momoOptions.NotifyUrl;
            returnUrl = momoOptions.ReturnUrl;
        }
    }
}