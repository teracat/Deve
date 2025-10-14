using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Azure.Monitor.OpenTelemetry.Exporter;
using Deve.Logging;

namespace Deve.Diagnostics
{
    public static class ServiceCollectionOpenTelemetryExtensions
    {
        private const string DiagnosticsProvider = "OpenTelemetry.Maui";

        public static MauiAppBuilder AddDiagnosticsOpenTelemetry(this MauiAppBuilder builder, Action<TracerProviderBuilder>? funcConfigTracing)
        {
            Log.Debug("{DiagnosticsProvider} - Configuring diagnostics...", DiagnosticsProvider);

            var zipkinUrl = Environment.GetEnvironmentVariable("ZIPKIN_URL");
            var azureAppInsightsConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");

            Log.Debug($"APPLICATIONINSIGHTS_CONNECTION_STRING={azureAppInsightsConnectionString}");

            builder.Logging.AddOpenTelemetry(options =>
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
                                  metrics.AddRuntimeInstrumentation();
                                  metrics.AddHttpClientInstrumentation();
                              })
                              .WithTracing(tracing =>
                              {
                                  tracing.AddHttpClientInstrumentation();

                                  if (!string.IsNullOrWhiteSpace(zipkinUrl))
                                  {
                                      Log.Debug("{DiagnosticsProvider} - Enabling Zipkin exporter for tracing...", DiagnosticsProvider);

                                      tracing.AddZipkinExporter(zipkin =>
                                      {
                                          zipkin.Endpoint = new Uri($"{zipkinUrl}/api/v2/spans");
                                      });
                                  }      

                                  funcConfigTracing?.Invoke(tracing);
                              });

            if (!string.IsNullOrWhiteSpace(azureAppInsightsConnectionString))
            {
                Log.Debug("{DiagnosticsProvider} - Enabling Azure Monitor...", DiagnosticsProvider);

                otel.UseAzureMonitorExporter(options =>
                {
                    options.ConnectionString = azureAppInsightsConnectionString;
                });
            }

            return builder;
        }
    }
}
