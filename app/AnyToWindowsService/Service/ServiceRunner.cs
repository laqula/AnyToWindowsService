using AnyToWindowsService.Settings;
using Topshelf;

namespace AnyToWindowsService.Service
{
    class ServiceRunner
    {
        public static TopshelfExitCode Run(AppSettings settings)
        {
            return HostFactory.Run(x =>
            {
                x.Service(sc => new RunnerService(settings));
                x.SetServiceName(settings.serviceName);
                x.SetDescription(settings.serviceDescription);
                x.SetDisplayName(settings.displayName);
                x.StartAutomatically();
            });
        }
    }
}
