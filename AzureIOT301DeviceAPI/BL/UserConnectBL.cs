using AzureIOT301DeviceAPI.Model;
using AzureIOT301DeviceAPI.Services;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

namespace AzureIOT301DeviceAPI.BL
{
    public class UserConnectBL : IUserConnect
    {
        private readonly IConfiguration _configuration;

        public UserConnectBL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ReportConnectivity(UserConnectUserData userConnectUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;DeviceId={1};SharedAccessKey={2}",
                                userConnectUserData.iothubname, userConnectUserData.deviceId, userConnectUserData.iotDeviceAccessKey);


            var Client = DeviceClient.CreateFromConnectionString(connectionString,
                                                Microsoft.Azure.Devices.Client.TransportType.Mqtt);

            Microsoft.Azure.Devices.Shared.TwinCollection reportedProperties;
            reportedProperties = new Microsoft.Azure.Devices.Shared.TwinCollection();
            reportedProperties["connectivity"] = userConnectUserData.reportPropertyValue;
            await Client.UpdateReportedPropertiesAsync(reportedProperties);

            return "Report Property Updated";

        }
        public async Task<string> SendDeviceToCloudMessagesAsync(UserConnectUserData userConnectUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;DeviceId={1};SharedAccessKey={2}",
                               userConnectUserData.iothubname, userConnectUserData.deviceId, userConnectUserData.iotDeviceAccessKey);


            DeviceClient s_deviceClient = DeviceClient.CreateFromConnectionString(connectionString, Microsoft.Azure.Devices.Client.TransportType.Mqtt);

            double minTemperature = 20;
            double minHumidity = 60;
            Random rand = new Random();

            //while (true)
            //{
            double currentTemperature = minTemperature + rand.NextDouble() * 15;
            double currentHumidity = minHumidity + rand.NextDouble() * 20;

            // Create JSON message  

            var telemetryDataPoint = new
            {

                temperature = currentTemperature,
                humidity = currentHumidity
            };

            string messageString = "";



            messageString = JsonConvert.SerializeObject(telemetryDataPoint);

            var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

            // Add a custom application property to the message.  
            // An IoT hub can filter on these properties without access to the message body.  
            //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");  

            // Send the telemetry message  
            await s_deviceClient.SendEventAsync(message);
            Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
            //await Task.Delay(1000 * 10);

            //}

            return "telemetry Message Send - "  + messageString;
        }
    }
}
