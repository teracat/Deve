using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using StackExchange.Redis;
using Deve.Logging;

namespace Deve.Diagnostics;

public static class OpenTelemetryWebApplicationBuilderExtensions
{
    private const string DiagnosticsProvider = "OpenTelemetry.AspNetCore";

    public static WebApplicationBuilder AddDiagnosticsOpenTelemetry(this WebApplicationBuilder builder, ConnectionMultiplexer? redisConnectionMultiplexer, ILog? log, Action<MeterProviderBuilder>? funcConfigMetrics, Action<TracerProviderBuilder>? funcConfigTracing)
    {
        log?.Debug("{DiagnosticsProvider} - Configuring diagnostics...", DiagnosticsProvider);

        var tracingOtlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
        var azureAppInsightsConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
        var prometheusScrapeEndpoint = builder.Configuration["PROMETHEUS_SCRAPE_ENDPOINT"];

        log?.Debug($"RedisConnectionMultiplexer.Configuration={redisConnectionMultiplexer?.Configuration}");
        log?.Debug($"OTEL_EXPORTER_OTLP_ENDPOINT={tracingOtlpEndpoint}");
        log?.Debug($"APPLICATIONINSIGHTS_CONNECTION_STRING={azureAppInsightsConnectionString}");
        log?.Debug($"PROMETHEUS_SCRAPE_ENDPOINT={prometheusScrapeEndpoint}");

        // Logs: record individual operations, such as an incoming request, a failure in a specific component, or an order being placed.
        _ = builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.ParseStateValues = true;
            options.IncludeFormattedMessage = true;
        });

        var otel = builder.Services
                          .AddOpenTelemetry();

        // Metrics: measuring counters and gauges such as number of completed requests, active requests, widgets that have been sold; or a histogram of the request latency.
        _ = otel
            .WithMetrics(metrics =>
            {
                _ = metrics
                    // Enable to collect incoming HTTP requests
                    .AddAspNetCoreInstrumentation()
                    // Enable to collect CPU, Memory, GC, and other runtime metrics
                    .AddRuntimeInstrumentation()
                    // Enable to collect RSS, threads, CPUs…
                    .AddProcessInstrumentation()
                    // Enable to collect outgoing HTTP client requests
                    .AddHttpClientInstrumentation()
                    // Metrics provided by ASP.NET Core in .NET
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    // Metrics provided by System.Net libraries
                    .AddMeter("System.Net.Http")
                    .AddMeter("System.Net.NameResolution");

                // Prometheus exporter
                if (!string.IsNullOrWhiteSpace(prometheusScrapeEndpoint))
                {
                    log?.Debug("{DiagnosticsProvider} - Enabling Prometheus exporter for metrics...", DiagnosticsProvider);

                    // Expose the Prometheus scrape endpoint
                    _ = metrics.AddPrometheusExporter(o => o.ScrapeEndpointPath = prometheusScrapeEndpoint);
                }

                // Allow additional configuration for metrics
                funcConfigMetrics?.Invoke(metrics);
            });

        // Tracing: tracks requests and activities across components in a distributed system so that you can see where time is spent and track down specific failures.
        _ = otel
            .WithTracing(tracing =>
            {
                _ = tracing
                    // Enable to collect incoming HTTP requests
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;

                        if (!string.IsNullOrWhiteSpace(prometheusScrapeEndpoint))
                        {
                            options.Filter = httpContext =>
                            {
                                // We do not want to trace Prometheus scrapes
                                return !httpContext.Request.Path.Equals(prometheusScrapeEndpoint, StringComparison.OrdinalIgnoreCase);
                            };
                        }
                    })
                    // Enable to collect outgoing HTTP client requests
                    .AddHttpClientInstrumentation();

                // Enable to collect redis calls
                if (redisConnectionMultiplexer is not null)
                {
                    log?.Debug("{DiagnosticsProvider} - Enabling Redis instrumentation for tracing...", DiagnosticsProvider);

                    _ = tracing.AddRedisInstrumentation(redisConnectionMultiplexer, options =>
                    {
                        options.Enrich = (activity, _) => activity.SetTag("peer.service", "redis");
                    });
                }

                // Allow additional configuration for tracing
                funcConfigTracing?.Invoke(tracing);
            });

        // Add Azure Monitor
        if (!string.IsNullOrWhiteSpace(azureAppInsightsConnectionString))
        {
            log?.Debug("{DiagnosticsProvider} - Enabling Azure Monitor...", DiagnosticsProvider);

            _ = otel.UseAzureMonitor(o => o.ConnectionString = azureAppInsightsConnectionString);
        }

        // Add OTLP exporter
        if (!string.IsNullOrEmpty(tracingOtlpEndpoint))
        {
            log?.Debug("{DiagnosticsProvider} - Enabling OTLP exporter...", DiagnosticsProvider);

            _ = otel.UseOtlpExporter();
        }

        return builder;
    }
}
