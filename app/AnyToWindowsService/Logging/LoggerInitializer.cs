using Microsoft.Extensions.Configuration;
using Serilog;

namespace AnyToWindowsService.Logging
{
    class LoggerInitializer
    {
        public static void Init()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.serilog.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
