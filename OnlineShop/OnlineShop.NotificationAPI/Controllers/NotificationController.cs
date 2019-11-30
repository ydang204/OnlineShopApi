using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;

namespace OnlineShop.NotificationAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
    }
}