namespace AzureIOT301DeviceAPI.Model
{
    public class DeviceUserData
    {
        public DeviceUserData()
        {
            deviceId = String.Empty;
            iotDeviceAccessKey = String.Empty;
            iothubname = String.Empty;
            iothubowner =String.Empty;
            desiredPropertyValue = String.Empty;    
        }
        public string deviceId { get; set; }
        public string iothubname { get; set; }

        public string iothubowner { get; set; }

        public string iotDeviceAccessKey { get; set; }
        public string desiredPropertyValue { get; set; }
    }
}
