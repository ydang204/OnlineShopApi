using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Models.Notification;
using OnlineShop.Common.Models.Notification.ReqModels;
using OnlineShop.NotificationAPI.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    //[Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DevicesController(IDeviceService deviceService,
                                 IMapper mapper)
        {
            _deviceService = deviceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice(AddDeviceReqModel model)
        {
            var device = _mapper.Map<AddDeviceReqModel, Device>(model);
            await _deviceService.AddDeviceAsync(device);
            return Ok(new { addDeviceSucceed = true });
        }

        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> DeleteDevice(Guid deviceId)
        {
            await _deviceService.DeleteDeviceByIdAsync(deviceId);
            return Ok(new { deleteDeviceSucceed = true });
        }

        [HttpDelete("delete-by-unique-id/{uniqueId}")]
        public async Task<IActionResult> DeleteDeviceByUniqueId(string uniqueId)
        {
            await _deviceService.DeleteDeviceByUniqueIdAsync(uniqueId);
            return Ok(new { deleteDeviceSucceed = true });
        }
    }
}