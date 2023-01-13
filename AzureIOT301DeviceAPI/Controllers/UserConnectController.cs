using AzureIOT301DeviceAPI.Model;
using AzureIOT301DeviceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureIOT301DeviceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserConnectController : ControllerBase
    {
        private readonly IUserConnect _userConnect;

        public UserConnectController(IUserConnect userConnect)
        {
            _userConnect = userConnect ?? throw new ArgumentNullException(nameof(_userConnect));
        }

        [HttpPost]
        [Route("ReportConnectivity")]
        public async Task<IActionResult> ReportConnectivity(UserConnectUserData userConnectUserData)
        {
            return Ok(await _userConnect.ReportConnectivity(userConnectUserData));
        }

        [HttpPost]
        [Route("SendDeviceToCloudMessages")]
        public async Task<IActionResult> SendDeviceToCloudMessagesAsync(UserConnectUserData userConnectUserData)
        {
            return Ok(await _userConnect.SendDeviceToCloudMessagesAsync(userConnectUserData));
        }
    }
}
