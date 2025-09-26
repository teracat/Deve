using System.Reflection;
using Deve.Internal.Data;
using Deve.Internal.Sdk;
using Deve.Sdk.LoggingHandlers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Services;
using Deve.Clients.Maui.Views;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Helpers
{
    public static class ServiceProviderHelper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, MauiNavigationService>();

            var handler = new LoggingHandlerLog();
            // Use SentryHttpMessageHandler to capture HTTP requests in Sentry
            //var handler = new SentryHttpMessageHandler(new LoggingHandlerLog());    // You can also chain handlers to add logging

            services.AddSingleton<IData, SdkMain>(provider => new SdkMain(Sdk.EnvironmentType.Staging, handler));
            services.AddSingleton<ISchedulerProvider, SchedulerProvider>();
        }

        public static void RegisterViewModels(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModels = assembly.GetTypes()
                                     .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseViewModel)));
            foreach (var vm in viewModels)
            {
                services.AddTransient(vm);
            }
        }

        public static void RegisterViews(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var views = assembly.GetTypes()
                                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseView)));
            foreach (var v in views)
            {
                services.AddTransient(v);
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
