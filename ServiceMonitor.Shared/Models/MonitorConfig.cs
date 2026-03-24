namespace ServiceMonitor.Shared.Models
{
    public class MonitorConfig
    {
        public int HttpPort { get; set; } = 8080;

        public List<MonitoredService> Services { get; set; } = new();

        public string ProductName { get; set; } = "Service Monitor";
        public string LogoPath { get; set; } = "";
    }
}