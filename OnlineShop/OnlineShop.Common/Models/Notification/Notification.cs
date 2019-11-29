namespace OnlineShop.Common.Models.Notification
{
    public class Notification : BaseEntity<int>
    {
        public int AccountId { get; set; }

        public string Data { get; set; }

        public int? DataId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}