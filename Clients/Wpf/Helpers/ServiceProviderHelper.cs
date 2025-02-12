using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Services;
using Deve.Clients.Wpf.Views;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Helpers
{
    public static class ServiceProviderHelper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IMessageHandler, MessageHandlerMessageBox>();
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
    }
}