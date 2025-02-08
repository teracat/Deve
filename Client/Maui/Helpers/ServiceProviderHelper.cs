using System.Reflection;
using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Services;
using Deve.ClientApp.Maui.Views;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Helpers
{
    public static class ServiceProviderHelper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, MauiNavigationService>();
            services.AddSingleton<IDataService, DataService>();
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
