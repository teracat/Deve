using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Services;
using Deve.Clients.Wpf.ViewModels;
using Deve.Clients.Wpf.Views;
using Deve.Core;
using Deve.Data;

namespace Deve.Clients.Wpf.Helpers;

internal static class ServiceProviderHelper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services.AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IMessageHandler, MessageBoxMessageHandler>()
                .AddConfigurationAppSettings()
                .AddSingleton<ISchedulerProvider, SchedulerProvider>()
                .AddCoreEmbedded(new DataOptions()
                {
                    LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
                });

    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var viewModels = assembly.GetTypes()
                                 .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseViewModel)));
        foreach (var vm in viewModels)
        {
            _ = services.AddTransient(vm);
        }
        return services;
    }

    public static IServiceCollection RegisterViews(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var views = assembly.GetTypes()
                            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseView)));
        foreach (var v in views)
        {
            _ = services.AddTransient(v);
        }
        return services;
    }
}
