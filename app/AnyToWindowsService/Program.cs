using AnyToWindowsService.Logging;
using AnyToWindowsService.Service;
using AnyToWindowsService.Settings;

public class Program
{
    static void Main(string[] args)
    {
        var config = SettingsInitializer.Init();
        LoggerInitializer.Init();
        ServiceRunner.Run(config);
    }
}