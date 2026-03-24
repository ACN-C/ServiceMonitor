using ServiceMonitor.Service.Monitoring;
using ServiceMonitor.Shared.Models;
using System.ServiceProcess;

namespace ServiceMonitor.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MonitorConfig _config;
        private readonly ServiceStatusCache _cache;


        // Inject logger and config (assuming MonitorConfig is registered in DI)
        public Worker(
            ILogger<Worker> logger,
            MonitorConfig config,
            ServiceStatusCache cache)
        {
            _logger = logger;
            _config = config;
            _cache = cache;
        }

        private void CheckService(string serviceName)
        {
            try
            {
                using var sc = new ServiceController(serviceName);
                var status = sc.Status.ToString();

                _cache.Update(serviceName, status);
            }
            catch
            {
                _cache.Update(serviceName, "Unknown");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var svc in _config.Services)
                {
                    CheckService(svc.ServiceName);

                    try
                    {
                        using var serviceController = new ServiceController(svc.ServiceName);

                        var status = serviceController.Status.ToString();

                        _cache.Update(svc.ServiceName, status);

                        _logger.LogInformation(
                            "Service '{service}' status: {status}",
                            svc.ServiceName,
                            status);
                    }
                    catch (Exception ex)
                    {
                        _cache.Update(svc.ServiceName, "Unknown");

                        _logger.LogError(ex, "Error checking service '{service}'", svc.ServiceName);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}