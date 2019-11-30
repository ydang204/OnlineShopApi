using System;

namespace OnlineShop.Common.Models.Notification
{
    public enum DevicePlatform
    {
        Web,
        Android,
        iOS
    }

    public class Device : BaseEntity<Guid>
    {
        public string Token { get; set; }

        public string DeviceUniqueIdentify { get; set; }

        public DevicePlatform Platform { get; set; }

        public int AccountId { get; set; }
    }
}