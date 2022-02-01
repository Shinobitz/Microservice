using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(plat), Encoding.UTF8, "application/json");

            // post content to the target server then it will return responce code(responseStatusCode), success or not.
            // parameter: Where to send the platform, and what
            var responseStatusCode = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);
            if (responseStatusCode.IsSuccessStatusCode)
            {
                Console.WriteLine("---> Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("---> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}