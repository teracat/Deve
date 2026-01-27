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
    public static void RegisterServices(IServiceCollection services)
    {
        _ = services.AddSingleton<INavigationService, NavigationService>();
        _ = services.AddSingleton<IMessageHandler, MessageBoxMessageHandler>();
        _ = services.AddCoreEmbedded(new DataOptions()
        {
            LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
        });
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
}
