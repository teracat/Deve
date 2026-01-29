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

    public static MauiAppBuilder AddDiagnosticsOpenTelemetry(this MauiAppBuilder builder, string? azureAppInsightsConnectionString, Uri? zipkinUrl, Action<TracerProviderBuilder>? funcConfigTracing)
    {
        Log.Debug("{DiagnosticsProvider} - Configuring diagnostics...", DiagnosticsProvider);

        Log.Debug($"APPLICATIONINSIGHTS_CONNECTION_STRING={azureAppInsightsConnectionString}");
        Log.Debug($"ZIPKIN_URL={zipkinUrl}");

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

                              if (zipkinUrl is not null)
                              {
                                  Log.Debug("{DiagnosticsProvider} - Enabling Zipkin exporter for tracing...", DiagnosticsProvider);

                                  _ = tracing.AddZipkinExporter(zipkin => zipkin.Endpoint = new Uri($"{zipkinUrl}/api/v2/spans"));
                              }

                              funcConfigTracing?.Invoke(tracing);
                          });

        if (!string.IsNullOrWhiteSpace(azureAppInsightsConnectionString))
        {
            Log.Debug("{DiagnosticsProvider} - Enabling Azure Monitor...", DiagnosticsProvider);

            _ = otel.UseAzureMonitorExporter(options => options.ConnectionString = azureAppInsightsConnectionString);
        }

        return builder;
    }
}
