using Microsoft.Extensions.Configuration;

namespace BlazorServerAppointmentApp.Model
{
    public class AppSettingsModel
    {
        public string WebApiUrl { get; set; }
        public string WebApiIp { get; set; }
        public string WebApiPort { get; set; }
        public string WebApiProtocol { get; set; }

        public AppSettingsModel(IConfiguration configurationSection)
        {
            WebApiUrl = configurationSection["WebApiUrl"];
            WebApiIp = configurationSection["WebApiIp"];
            WebApiPort = configurationSection["WebApiPort"];
            WebApiProtocol = configurationSection["WebApiProtocol"];
        }
    }
}