using AutoMapper;
using OnlineShop.Common.Models.Notification;
using OnlineShop.Common.Models.Notification.ReqModels;

namespace OnlineShop.NotificationAPI.MappingProfiles
{
    public class DeviceMappingProfile : Profile
    {
        public DeviceMappingProfile()
        {
            CreateMap<AddDeviceReqModel, Device>();
        }
    }
}