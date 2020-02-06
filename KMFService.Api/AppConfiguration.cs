using Microsoft.Extensions.Configuration;

namespace KMFService.Api
{
    public class AppConfiguration
    {
        public string SqlConnection { get; set; }
    }

    public static class AppConfigurationExtension
    {
        public static AppConfiguration RegisterConfig(this IConfiguration configuration)
        {
            return new AppConfiguration
            {
                SqlConnection = configuration.GetConnectionString("Currency")
            };
        }
    }
}