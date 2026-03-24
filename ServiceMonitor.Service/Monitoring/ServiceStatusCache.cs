using System.Collections.Concurrent;

namespace ServiceMonitor.Service.Monitoring
{
    public class ServiceStatusCache
    {
        private readonly ConcurrentDictionary<string, string> _statuses = new();

        public void Update(string serviceName, string status)
        {
            _statuses[serviceName] = status;
        }

        public string? Get(string serviceName)
        {
            _statuses.TryGetValue(serviceName, out var status);
            return status;
        }

        public Dictionary<string, string> GetAll()
        {
            return new Dictionary<string, string>(_statuses);
        }
    }
}
