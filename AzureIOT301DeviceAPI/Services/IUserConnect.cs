using AzureIOT301DeviceAPI.Model;

namespace AzureIOT301DeviceAPI.Services
{
    public interface IUserConnect
    {
        Task<string> ReportConnectivity(UserConnectUserData userConnectUserData);
        Task<string> SendDeviceToCloudMessagesAsync(UserConnectUserData userConnectUserData);
    }
}
