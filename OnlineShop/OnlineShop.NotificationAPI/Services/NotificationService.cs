using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Exceptions;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.Notification;
using OnlineShop.Common.Resources;
using OnlineShop.NotificationAPI.Models;
using OnlineShop.NotificationAPI.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationContext _context;
        private readonly HttpContext _httpContext;

        public NotificationService(NotificationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task AddDeviceAsync(Device device)
        {
            var accountId = _httpContext.User.GetAccountId().Value;
            var existDevice = await _context.Devices
                                            .FirstOrDefaultAsync(d => (d.Token.ToLower()
                                            .Equals(device.Token) || d.DeviceUniqueIdentify.ToLower()
                                            .Equals(device.DeviceUniqueIdentify.ToLower())) && d.AccountId == accountId);

            if (existDevice != null)
            {
                throw new CustomException(Errors.DEVICE_ALREADY_EXIST, Errors.DEVICE_ALREADY_EXIST_MSG);
            }

            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceByIdAsync(Guid deviceId)
        {
            var existDevice = await _context.Devices
                                             .FirstOrDefaultAsync(d => d.Id == deviceId);

            if (existDevice == null)
            {
                throw new CustomException(Errors.DEVICE_DOES_NOT_EXIST, Errors.DEVICE_DOES_NOT_EXIST_MSG);
            }

            _context.Devices.Remove(existDevice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceByUniqueIdAsync(string uniqueId)
        {
            var existDevice = await _context.Devices
                                             .FirstOrDefaultAsync(d => d.DeviceUniqueIdentify == uniqueId);

            if (existDevice == null)
            {
                throw new CustomException(Errors.DEVICE_DOES_NOT_EXIST, Errors.DEVICE_DOES_NOT_EXIST_MSG);
            }

            _context.Devices.Remove(existDevice);
            await _context.SaveChangesAsync();
        }
    }
}