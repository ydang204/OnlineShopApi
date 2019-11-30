namespace OnlineShop.Common.Models.Notification.ReqModels
{
    public class AddDeviceReqModel
    {
        public string Token { get; set; }

        public string DeviceUniqueIdentify { get; set; }

        public DevicePlatform Platform { get; set; }
    }
}