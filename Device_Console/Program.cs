using Microsoft.Azure.Devices.Client;
using System.Net.Http.Json;
using System.Text;


namespace Device_Console
{
    class Program
    {
        private static DeviceClient _deviceClient;
        private static string _deviceId = "b48de464-3bc6-4121-b43c-2ecc568241f7";
        public static async Task Main()
        {
            await InitializeAsync(_deviceClient, _deviceId);
        }

        private static async Task InitializeAsync(DeviceClient deviceClient, string deviceId)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync(
                $"https://function-app-api.azurewebsites.net/api/GetDevice?code=IDPaGA4bbYvY1liOlosh7LNStAtA_clgmyv6cnVNhss_AzFuUbjhJA==&deviceId={deviceId}");
            var connectionString = await result.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(connectionString))
            {
                result = await client.PostAsJsonAsync(
                    "https://function-app-api.azurewebsites.net/api/CreateDevice?code=AsPPnkW3IOvObx73iy0NhyN7mBitR1yn47jr439wGXjqAzFurh2sMw==", new { deviceId = deviceId });
                connectionString = await result.Content.ReadAsStringAsync();
            }

            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
            Console.WriteLine($"Device Connection: {connectionString}");
            Console.WriteLine("Device Connected");

        }
    }
}











