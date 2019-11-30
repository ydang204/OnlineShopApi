using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Models.Notification;
using OnlineShop.Common.Models.Notification.ReqModels;
using OnlineShop.NotificationAPI.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public DevicesController(INotificationService notificationService,
                                 IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice(AddDeviceReqModel model)
        {
            var device = _mapper.Map<AddDeviceReqModel, Device>(model);
            await _notificationService.AddDeviceAsync(device);
            return Ok(new { addDeviceSucceed = true });
        }

        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> DeleteDevice(Guid deviceId)
        {
            await _notificationService.DeleteDeviceByIdAsync(deviceId);
            return Ok(new { deleteDeviceSucceed = true });
        }

        [HttpDelete("delete-by-unique-id/{uniqueId}")]
        public async Task<IActionResult> DeleteDeviceByUniqueId(string uniqueId)
        {
            await _notificationService.DeleteDeviceByUniqueIdAsync(uniqueId);
            return Ok(new { deleteDeviceSucceed = true });
        }
    }
}