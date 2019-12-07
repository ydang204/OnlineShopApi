using Newtonsoft.Json.Linq;
using OnlineShop.Common.Utitlities;

namespace OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment
{
    public class PaymentDataReqModel
    {
        private MoMoSecurity crypto = new MoMoSecurity();

        public string AccessKey { get; set; }

        public string PartnerCode { get; set; }

        public string RequestType { get; set; }

        public string NotifyUrl { get; set; }

        public string ReturnUrl { get; set; }

        public string OrderId { get; set; }

        public string Amount { get; set; }

        public string OrderInfo { get; set; }

        public string RequestId { get; set; }

        public string ExtraData { get; set; }

        public JObject getDataJsonObject()
        {
            string rawHash = "partnerCode=" +
            this.PartnerCode + "&accessKey=" +
            this.AccessKey + "&requestId=" +
            this.RequestId + "&amount=" +
            this.Amount + "&orderId=" +
            this.OrderId + "&orderInfo=" +
            this.OrderInfo + "&returnUrl=" +
            this.ReturnUrl + "&notifyUrl=" +
            this.NotifyUrl + "&extraData=" +
            this.ExtraData;

            //sign signature SHA256
            string SignatureScripted = this.crypto.signSHA256(rawHash, Constants.SharedContants.SERECT_KEY);

            return new JObject
            {
                { "partnerCode", this.PartnerCode },
                { "accessKey", this.AccessKey },
                { "requestId", this.RequestId },
                { "amount", this.Amount },
                { "orderId", this.OrderId },
                { "orderInfo", this.OrderInfo },
                { "returnUrl", this.ReturnUrl },
                { "notifyUrl", this.NotifyUrl },
                { "extraData", this.ExtraData },
                { "requestType", this.RequestType },
                { "signature", SignatureScripted }
            };
        }
    }
}