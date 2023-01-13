using AzureIOT301DeviceAPI.Model;

namespace AzureIOT301DeviceAPI.Services
{
    public interface IDeviceConnect
    {
        Task<string> CreateDeviceAsync(DeviceUserData deviceUserData);
        Task<string> UpdateDeviceAsync(DeviceUserData deviceUserData);
        Task<string> GetDeviceAsync(DeviceUserData deviceUserData);
        Task<string> DeleteDeviceAsync(DeviceUserData deviceUserData);
        Task<string> AddTagsAndQueryAsync(DeviceUserData deviceUserData);
        Task<string> UpdateDesiredPropertyAsync(DeviceUserData deviceUserData);

    }
}
