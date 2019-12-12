namespace OnlineShop.Common.Models.OrderAPI
{
    public class OrderDetails : BaseEntity<int>
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}