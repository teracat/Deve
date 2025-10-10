using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Deve.Diagnostics
{
    public static class ServiceCollectionOpenTelemetryExtensions
    {
        public static MauiAppBuilder AddDiagnosticsOpenTelemetry(this MauiAppBuilder builder, Action<TracerProviderBuilder>? funcConfigTracing)
        {
            builder.Services
                   .AddOpenTelemetry()
                   .ConfigureResource(resource => resource
                                                  .AddService(serviceName: "app.maui"))
                   .WithMetrics(metrics =>
                   {
                       metrics.AddRuntimeInstrumentation();
                   })
                   .WithTracing(tracing =>
                   {
                       tracing.AddHttpClientInstrumentation()
                              .AddZipkinExporter(zipkin =>
                              {
                                  var zipkinUrl = builder.Configuration["ZIPKIN_URL"] ?? "http://localhost:9411";  // Replace with your Zipkin server URL
                                  zipkin.Endpoint = new Uri($"{zipkinUrl}/api/v2/spans");
                              });

                       funcConfigTracing?.Invoke(tracing);
                   });

            return builder;
        }
    }
}
