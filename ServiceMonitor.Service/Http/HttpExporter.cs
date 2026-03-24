using ServiceMonitor.Service.Monitoring;
using ServiceMonitor.Shared.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ServiceMonitor.Service.Http;

public class HttpExporter : BackgroundService
{
    private readonly ILogger<HttpExporter> _logger;
    private readonly MonitorConfig _config;
    private readonly ServiceStatusCache _cache;
    private HttpListener? _listener;

    public HttpExporter(
        ILogger<HttpExporter> logger,
        MonitorConfig config,
        ServiceStatusCache cache)
    {
        _logger = logger;
        _config = config;
        _cache = cache;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://+:{_config.HttpPort}/");

        _listener.Start();

        _logger.LogInformation("HTTP exporter listening on port {port}", _config.HttpPort);

        while (!stoppingToken.IsCancellationRequested)
        {
            var context = await _listener.GetContextAsync();

            _ = Task.Run(() => HandleRequest(context), stoppingToken);
        }
    }

    private async Task HandleRequest(HttpListenerContext context)
    {
        var path = context.Request.Url?.AbsolutePath?.ToLower() ?? "";

        try
        {
            if (path == "/status")
            {
                var data = _cache.GetAll();
                await WriteJson(context, data);
            }
            else if (path == "/config")
            {
                await WriteJson(context, _config);
            }
            else if (path.StartsWith("/status/"))
            {
                var serviceName = path.Replace("/status/", "");

                var status = _cache.Get(serviceName);

                if (status == null)
                {
                    context.Response.StatusCode = 404;
                    await WriteText(context, "Service not found");
                }
                else
                {
                    await WriteJson(context, new { service = serviceName, status });
                }
            }
            else
            {
                context.Response.StatusCode = 404;
                await WriteText(context, "Not found");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HTTP request failed");
        }
    }

    private async Task WriteJson(HttpListenerContext ctx, object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var buffer = Encoding.UTF8.GetBytes(json);

        ctx.Response.ContentType = "application/json";
        ctx.Response.ContentLength64 = buffer.Length;

        await ctx.Response.OutputStream.WriteAsync(buffer);
        ctx.Response.Close();
    }

    private async Task WriteText(HttpListenerContext ctx, string text)
    {
        var buffer = Encoding.UTF8.GetBytes(text);

        ctx.Response.ContentLength64 = buffer.Length;

        await ctx.Response.OutputStream.WriteAsync(buffer);
        ctx.Response.Close();
    }
}