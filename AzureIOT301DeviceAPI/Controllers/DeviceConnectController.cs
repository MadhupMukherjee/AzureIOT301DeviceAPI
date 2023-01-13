using AzureIOT301DeviceAPI.Model;
using AzureIOT301DeviceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureIOT301DeviceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceConnectController : ControllerBase
    {
        private readonly IDeviceConnect _deviceConnect;

        public DeviceConnectController(IDeviceConnect deviceConnect)
        {
            _deviceConnect = deviceConnect ?? throw new ArgumentNullException(nameof(_deviceConnect));
        }

        [HttpPost]
        [Route("CreateDevice")]
        public async Task<IActionResult> CreateDeviceAsync(DeviceUserData deviceUserData)
        {
            return Ok(await _deviceConnect.CreateDeviceAsync(deviceUserData));
        }

        [HttpPost]
        [Route("UpdateDevice")]
        public async Task<IActionResult> UpdateDeviceAsync(DeviceUserData deviceUserData)
        {
            return Ok(await _deviceConnect.UpdateDeviceAsync(deviceUserData));
        }
        [HttpPost]
        [Route("GetDevice")]
        public async Task<IActionResult> GetDeviceAsync(DeviceUserData deviceUserData)
        {
            return Ok(await _deviceConnect.GetDeviceAsync(deviceUserData));
        }
        [HttpPost]
        [Route("DeleteDevice")]
        public async Task<IActionResult> DeleteDeviceAsync(DeviceUserData deviceUserData)
        {
            return Ok(await _deviceConnect.DeleteDeviceAsync(deviceUserData));
        }
        [HttpPost]
        [Route("AddTagsAndQuery")]
        public async Task<IActionResult> AddTagsAndQueryAsync(DeviceUserData deviceUserData)
        {
            return Ok(await _deviceConnect.AddTagsAndQueryAsync(deviceUserData));
        }
        [HttpPost]
        [Route("UpdateDesiredProperty")]
        public async Task<IActionResult> UpdateDesiredPropertyAsync(DeviceUserData deviceUserData)
        {
            return Ok(await _deviceConnect.UpdateDesiredPropertyAsync(deviceUserData));
        }
    }
}
