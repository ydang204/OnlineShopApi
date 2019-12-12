namespace OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment
{
    public class PaymentReqModel
    {
        public string OrderId { get; set; }

        public string Amount { get; set; }

        public string OrderInfo { get; set; }

        public string RequestId { get; set; }

        public string ExtraData { get; set; }
    }
}