using System.Reflection;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Services;
using Deve.Clients.Maui.ViewModels;
using Deve.Clients.Maui.Views;
using Deve.Data;
using Deve.Diagnostics;
using Deve.Logging;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients.Maui.Helpers;

internal static class ServiceProviderHelper
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder, ILog log)
    {
        RegisterServices(mauiAppBuilder.Services, log);
        return mauiAppBuilder;
    }

    public static void RegisterServices(IServiceCollection services, ILog log)
    {
        _ = services.AddSingleton(log);
        _ = services.AddSingleton<INavigationService, MauiNavigationService>();

        // Register the OpenTelemetry transaction handler for diagnostics
        _ = services.AddSingleton<IDiagnosticsTransactionHandler, OpenTelemetryTransactionHandler>();
        _ = services.AddSingleton<IData>(provider => SdkBuilder.Create(EnvironmentType.Staging, new LogLoggingHandler(provider.GetRequiredService<ILog>())));

        // If you want to use Sentry, comment the previous two lines and uncomment the next two lines. Use SentryHttpMessageHandler to capture HTTP requests in Sentry.
        //_ = services.AddSingleton<IDiagnosticsTransactionHandler, SentryTransactionHandler>();
        //_ = services.AddSingleton<IData>(provider => SdkBuilder.Create(EnvironmentType.Staging, new SentryHttpMessageHandler(new LogLoggingHandler(provider.GetRequiredService<ILog>()))));
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        RegisterViewModels(mauiAppBuilder.Services);
        return mauiAppBuilder;
    }

    public static void RegisterViewModels(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var viewModels = assembly.GetTypes()
                                 .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseViewModel)));
        foreach (var vm in viewModels)
        {
            _ = services.AddTransient(vm);
        }
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        RegisterViews(mauiAppBuilder.Services);
        return mauiAppBuilder;
    }

    public static void RegisterViews(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var views = assembly.GetTypes()
                            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseView)));
        foreach (var v in views)
        {
            _ = services.AddTransient(v);
        }
    }
}
