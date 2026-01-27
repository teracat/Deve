using System.Reflection;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Services;
using Deve.Clients.Maui.ViewModels;
using Deve.Clients.Maui.Views;
using Deve.Data;
using Deve.Diagnostics;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients.Maui.Helpers;

internal static class ServiceProviderHelper
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        RegisterServices(mauiAppBuilder.Services);
        return mauiAppBuilder;
    }

    public static void RegisterServices(IServiceCollection services)
    {
        _ = services.AddSingleton<INavigationService, MauiNavigationService>();

        // Register the OpenTelemetry transaction handler for diagnostics
        _ = services.AddSingleton<IDiagnosticsTransactionHandler, OpenTelemetryTransactionHandler>();
        _ = services.AddSingleton<IData>(_ => SdkBuilder.Create(EnvironmentType.Staging, new LogLoggingHandler()));

        // If you want to use Sentry, comment the previous two lines and uncomment the next two lines. Use SentryHttpMessageHandler to capture HTTP requests in Sentry.
        //services.AddSingleton<IDiagnosticsTransactionHandler, TransactionHandlerSentry>();
        //services.AddSingleton<IData, SdkMain>(provider => new SdkMain(Sdk.EnvironmentType.Staging, new SentryHttpMessageHandler(new LoggingHandlerLog())));
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
