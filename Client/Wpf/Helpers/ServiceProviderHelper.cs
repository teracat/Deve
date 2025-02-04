using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Deve.ClientApp.Wpf.Interfaces;
using Deve.ClientApp.Wpf.Services;
using Deve.ClientApp.Wpf.Views;
using Deve.ClientApp.Wpf.ViewModels;

namespace Deve.ClientApp.Wpf.Helpers
{
    public static class ServiceProviderHelper
    {
        public static void RegisterServices(ServiceCollection services)
        {
            //services.AddSingleton<IServiceProvider, ServiceProvider>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDataService, DataService>();
        }

        public static void RegisterViewModels(ServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModels = assembly.GetTypes()
                                     .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseViewModel)));
            foreach (var vm in viewModels)
            {
                services.AddTransient(vm);
            }
        }

        public static void RegisterViews(ServiceCollection services)
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
