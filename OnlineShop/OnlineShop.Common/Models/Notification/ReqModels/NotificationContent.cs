namespace OnlineShop.Common.Models.Notification.ReqModels
{
    public class NotificationContent
    {
        public string ActionUrl { get; internal set; }
        public object Body { get; internal set; }
        public object Title { get; internal set; }
    }
}