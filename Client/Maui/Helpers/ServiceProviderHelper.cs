using System.Reflection;
using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Services;
using Deve.ClientApp.Maui.Views;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Helpers
{
    public static class ServiceProviderHelper
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IDataService, DataService>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModels = assembly.GetTypes()
                                     .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseViewModel)));
            foreach (var vm in viewModels)
            {
                mauiAppBuilder.Services.AddTransient(vm);
            }

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var views = assembly.GetTypes()
                                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseView)));
            foreach (var v in views)
            {
                mauiAppBuilder.Services.AddTransient(v);
            }

            return mauiAppBuilder;
        }
    }
}
