namespace OnlineShop.Common.Models.OrderAPI.ResModels
{
    public class CreateOrderResModel
    {
        public string PaymentUrl { get; set; }

        public OrderStatus Status { get; set; }
    }
}