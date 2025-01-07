using Microsoft.Extensions.Configuration;

namespace AutomationFramwork.API.Framework.Utilities
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot _configuration;

        static ConfigReader()
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "API"); // Adjust for API subfolder
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public static string GetBaseUrl()
        {
            var baseUrl = _configuration["BaseUrl"];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException("BaseUrl is not configured in the appsettings.json file.");
            }

            return baseUrl;
        }
    }
}
