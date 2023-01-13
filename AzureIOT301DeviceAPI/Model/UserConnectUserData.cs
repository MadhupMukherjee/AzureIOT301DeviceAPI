namespace AzureIOT301DeviceAPI.Model
{
    public class UserConnectUserData
    {
        public UserConnectUserData()
        {
            deviceId = String.Empty;
            iotDeviceAccessKey = String.Empty;
            iothubname = String.Empty;
            reportPropertyValue = String.Empty;
        }
        public string deviceId { get; set; }
        public string iothubname { get; set; }
        public string iotDeviceAccessKey { get; set; }
        public string reportPropertyValue { get; set; }
    }
}
