using OnlineShop.Common.Models.Notification;
using System;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.ServiceInterfaces
{
    public interface INotificationService
    {
        Task AddDeviceAsync(Device device);

        Task DeleteDeviceByIdAsync(Guid deviceId);

        Task DeleteDeviceByUniqueIdAsync(string uniqueId);
    }
}