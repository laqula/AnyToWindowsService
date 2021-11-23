using Microsoft.Extensions.Configuration;

namespace AnyToWindowsService.Settings
{
    class SettingsInitializer
    {
        public static AppSettings Init()
        {
            var builder = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
            return config.Get<AppSettings>();
            
        }
    }
}
