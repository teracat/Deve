using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Deve.Logging;

namespace Deve.Diagnostics;

public static class OpenTelemetryServiceCollectionExtensions
{
    private const string DiagnosticsProvider = "OpenTelemetry.Maui";

    public static MauiAppBuilder AddDiagnosticsOpenTelemetry(this MauiAppBuilder builder, string? azureAppInsightsConnectionString, ILog? log, Action<TracerProviderBuilder>? funcConfigTracing)
    {
        log?.Debug("{DiagnosticsProvider} - Configuring diagnostics...", DiagnosticsProvider);

        log?.Debug($"APPLICATIONINSIGHTS_CONNECTION_STRING={azureAppInsightsConnectionString}");

        _ = builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.ParseStateValues = true;
            options.IncludeFormattedMessage = true;
        });

        var otel = builder.Services
                          .AddOpenTelemetry()
                          .ConfigureResource(resource => resource
                                                         .AddService(serviceName: "app.maui"))
                          .WithMetrics(metrics =>
                          {
                              _ = metrics.AddRuntimeInstrumentation();
                              _ = metrics.AddHttpClientInstrumentation();
                          })
                          .WithTracing(tracing =>
                          {
                              _ = tracing.AddHttpClientInstrumentation();

                              funcConfigTracing?.Invoke(tracing);
                          });

        if (!string.IsNullOrWhiteSpace(azureAppInsightsConnectionString))
        {
            log?.Debug("{DiagnosticsProvider} - Enabling Azure Monitor...", DiagnosticsProvider);

            _ = otel.UseAzureMonitorExporter(options => options.ConnectionString = azureAppInsightsConnectionString);
        }

        return builder;
    }
}
