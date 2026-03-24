using ServiceMonitor.Service;
using ServiceMonitor.Service.Http;
using ServiceMonitor.Service.Monitoring;
using ServiceMonitor.Shared.Models;
using System.Text.Json;

var builder = Host.CreateApplicationBuilder(args);
 
var path = Path.Combine(AppContext.BaseDirectory, "config.json");
// Load config
var json = File.ReadAllText(path);
var config = JsonSerializer.Deserialize<MonitorConfig>(json);



// Register services
builder.Services.AddWindowsService();

if (config == null)
{
    throw new InvalidOperationException("Failed to deserialize MonitorConfig.");
}
builder.Services.AddSingleton(config);
builder.Services.AddSingleton<ServiceStatusCache>();

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<HttpExporter>();

var host = builder.Build();
host.Run();