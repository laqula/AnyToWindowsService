namespace AnyToWindowsService.Settings
{
    public class AppSettings
    {
        public string? serviceName { get; set; } = "AnyToWindowsService";
        public string? serviceDescription { get; set; } = "AnyToWindowsService des";
        public string? displayName { get; set; } = "AnyToWindowsService dn";
        public string? command { get; set; }
        public int? intervalInSec { get; set; }
        public bool? runOnlyOnce { get; set; }
        public int? maxWaitForFinishInSec { get; set; }
    }
}
