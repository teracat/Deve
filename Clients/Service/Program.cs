using Deve.Logging;

namespace Deve.Clients;

/// <summary>
/// Main Sdk Client Service program.
/// https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service
/// </summary>
internal static class Program
{
    public static void Main(string[] args)
    {
        MultiLog log = new();
        //log.AddLog4Net();
        log.AddNLog();
        //log.AddSerilog();
        //log.AddSentry();
        //-:cnd
#if DEBUG
        log.AddDebug();
#endif
        //+:cnd

        var builder = Host.CreateApplicationBuilder(args);
        _ = builder.Services.AddWindowsService(options => options.ServiceName = "Deve Client");
        _ = builder.Services.AddHostedService<Worker>();
        _ = builder.Services.AddSingleton<ILog>(log);

        var host = builder.Build();

        var logger = host.Services.GetRequiredService<ILogger<Worker>>();
        log.AddNetCore(logger);

        host.Run();
    }
}
