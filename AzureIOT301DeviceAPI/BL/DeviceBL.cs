using System.Text;
using AzureIOT301DeviceAPI.Model;
using AzureIOT301DeviceAPI.Services;
using Microsoft.Azure.Devices;


namespace AzureIOT301DeviceAPI.BL
{
    public class DeviceBL : IDeviceConnect
    {
        private readonly IConfiguration _configuration;

        public DeviceBL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateDeviceAsync(DeviceUserData deviceUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;SharedAccessKeyName={1};SharedAccessKey={2}",
                                deviceUserData.iothubname, deviceUserData.iothubowner, deviceUserData.iotDeviceAccessKey);

            Device device = new Device(deviceUserData.deviceId);
            Device newdevice = new Device();

            string connectionstring = string.Empty;

            var primaryKey = Guid.NewGuid();
            var secondaryKey = Guid.NewGuid();

            byte[] bytes = Encoding.UTF8.GetBytes(primaryKey.ToString());
            string base64PrimaryKey = Convert.ToBase64String(bytes);

            bytes = Encoding.UTF8.GetBytes(secondaryKey.ToString());
            string base64SecondaryKey = Convert.ToBase64String(bytes);

            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);

            device.Authentication = new AuthenticationMechanism
            {
                SymmetricKey = new SymmetricKey
                {
                    PrimaryKey = base64PrimaryKey,
                    SecondaryKey = base64SecondaryKey

                }

            };

            newdevice = await registryManager.AddDeviceAsync(device);
            connectionstring = string.Format("HostName={0};DeviceId={1};SharedAccessKey={2}",
                deviceUserData.iothubname + ".azure-devices.net", newdevice.Id, newdevice.Authentication.SymmetricKey.PrimaryKey);

            //token = newdevice.Authentication.SymmetricKey.PrimaryKey;

            return connectionstring;
        }

        public async Task<string> UpdateDeviceAsync(DeviceUserData deviceUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;SharedAccessKeyName={1};SharedAccessKey={2}",
                              deviceUserData.iothubname, deviceUserData.iothubowner, deviceUserData.iotDeviceAccessKey);

            string connectionstring = string.Empty;
            Device device = new Device();

            var primaryKey = Guid.NewGuid();
            var secondaryKey = Guid.NewGuid();

            byte[] bytes = Encoding.UTF8.GetBytes(primaryKey.ToString());
            string base64PrimaryKey = Convert.ToBase64String(bytes);

            bytes = Encoding.UTF8.GetBytes(secondaryKey.ToString());
            string base64SecondaryKey = Convert.ToBase64String(bytes);


            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceUserData.deviceId);

            device.Authentication = new AuthenticationMechanism
            {
                SymmetricKey = new SymmetricKey
                {
                    PrimaryKey = base64PrimaryKey,
                    SecondaryKey = base64SecondaryKey
                }
            };

            device = await registryManager.UpdateDeviceAsync(device);
            connectionstring = string.Format("HostName={0};DeviceId={1};SharedAccessKey={2}"
                , deviceUserData.iothubname + ".azure-devices.net", device.Id, device.Authentication.SymmetricKey.PrimaryKey);

            return connectionstring;
        }

        public async Task<string> GetDeviceAsync(DeviceUserData deviceUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;SharedAccessKeyName={1};SharedAccessKey={2}",
                             deviceUserData.iothubname, deviceUserData.iothubowner, deviceUserData.iotDeviceAccessKey);

            string connectionstring = string.Empty;
            Device device = new Device();
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceUserData.deviceId);

            connectionstring = string.Format("HostName={0};DeviceId={1};SharedAccessKey={2}", deviceUserData.iothubname + ".azure-devices.net", device.Id, device.Authentication.SymmetricKey.PrimaryKey);

            return connectionstring;

        }

        public async Task<string> DeleteDeviceAsync(DeviceUserData deviceUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;SharedAccessKeyName={1};SharedAccessKey={2}",
                            deviceUserData.iothubname, deviceUserData.iothubowner, deviceUserData.iotDeviceAccessKey);

            string connectionstring = string.Empty;
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceUserData.deviceId);
            connectionstring = string.Format("Delete Successful deviceID - {0} ", deviceUserData.deviceId);

            return connectionstring;
        }


        public async Task<string> AddTagsAndQueryAsync(DeviceUserData deviceUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;SharedAccessKeyName={1};SharedAccessKey={2}",
                           deviceUserData.iothubname, deviceUserData.iothubowner, deviceUserData.iotDeviceAccessKey);

            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            var twin = await registryManager.GetTwinAsync(deviceUserData.deviceId);
            var patch =
                @"{
                    tags: {
                    location: {
                        region: 'US',
                        plant: 'Redmond43'
                              }
                          }
                  }";
            await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);

            var query = registryManager.CreateQuery(
              "SELECT * FROM devices WHERE tags.location.plant = 'Redmond43'", 100);
            var twinsInRedmond43 = await query.GetNextAsTwinAsync();
            Console.WriteLine("Devices in Redmond43: {0}",
              string.Join(", ", twinsInRedmond43.Select(t => t.DeviceId)));

            query = registryManager.CreateQuery("SELECT * FROM devices WHERE tags.location.plant = 'Redmond43' AND properties.reported.connectivity = '1'", 100);
            var twinsInRedmond43UsingCellular = await query.GetNextAsTwinAsync();
            Console.WriteLine("Devices in Redmond43 using Connectivity: {0}",
              string.Join(", ", twinsInRedmond43UsingCellular.Select(t => t.DeviceId)));

            return "Tag Property Updated";

        }

        public async Task<string> UpdateDesiredPropertyAsync(DeviceUserData deviceUserData)
        {
            string connectionString = string.Format("HostName={0}.azure-devices.net;SharedAccessKeyName={1};SharedAccessKey={2}",
                           deviceUserData.iothubname, deviceUserData.iothubowner, deviceUserData.iotDeviceAccessKey);

            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            var twin = await registryManager.GetTwinAsync(deviceUserData.deviceId);
            twin.Properties.Desired["devicedesiredproperty"] = deviceUserData.desiredPropertyValue;
            await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);

            return "Desired Property Updated";

        }


    }
}
