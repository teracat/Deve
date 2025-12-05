using System.Reflection;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Services;
using Deve.Clients.Maui.ViewModels;
using Deve.Clients.Maui.Views;
using Deve.Diagnostics;
using Deve.Internal.Data;
using Deve.Internal.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients.Maui.Helpers
{
    public static class ServiceProviderHelper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            _ = services.AddSingleton<INavigationService, MauiNavigationService>();

            // Register the OpenTelemetry transaction handler for diagnostics
            _ = services.AddSingleton<IDiagnosticsTransactionHandler, TransactionHandlerOpenTelemetry>();
            _ = services.AddSingleton<IData, SdkMain>(provider => new SdkMain(Sdk.EnvironmentType.Staging, new LoggingHandlerLog()));

            // If you want to use Sentry, comment the previous two lines and uncomment the next two lines. Use SentryHttpMessageHandler to capture HTTP requests in Sentry.
            //services.AddSingleton<IDiagnosticsTransactionHandler, TransactionHandlerSentry>();
            //services.AddSingleton<IData, SdkMain>(provider => new SdkMain(Sdk.EnvironmentType.Staging, new SentryHttpMessageHandler(new LoggingHandlerLog())));
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

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            RegisterServices(mauiAppBuilder.Services);
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            RegisterViewModels(mauiAppBuilder.Services);
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            RegisterViews(mauiAppBuilder.Services);
            return mauiAppBuilder;
        }
    }
}
