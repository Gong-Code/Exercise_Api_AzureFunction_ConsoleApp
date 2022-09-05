using Microsoft.Azure.Devices;

namespace WebApi.Sevices
{
    public interface IDeviceService
    {
        public Task<string> CreateDeviceAsync(string deviceId);
        public Task<Device> GetDeviceAsync(string deviceId);
    }

    public class DeviceService : IDeviceService
    {
        private readonly RegistryManager _registryManager;
        private readonly IConfiguration _configuration;

        public DeviceService(IConfiguration configuration)
        {
            _configuration = configuration;
            _registryManager = RegistryManager.CreateFromConnectionString(_configuration.GetConnectionString("IotHub"));
        }

        public async Task<string> CreateDeviceAsync(string deviceId)
        {
            var device = await _registryManager.AddDeviceAsync(new Device(deviceId));
            
            return $"HostName=systemDev-IotHub.azure-devices.net;DeviceId={device.Id};SharedAccessKey{device.Authentication.SymmetricKey.PrimaryKey}";
        }

        public Task<Device> GetDeviceAsync(string deviceId)
        {
            throw new NotImplementedException();
        }
    }
}
