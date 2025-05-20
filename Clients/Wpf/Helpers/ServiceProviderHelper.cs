using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Deve.Data;
using Deve.DataSource;
using Deve.DataSource.Config;
using Deve.Auth;
using Deve.Auth.Hash;
using Deve.Auth.TokenManagers;
using Deve.Auth.UserIdentityService;
using Deve.Core;
using Deve.Cache;
using Deve.Internal.Data;
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
            services.AddSingleton<IMessageHandler, MessageHandlerMessageBox>();

            services.AddSingleton<IHash, HashSha512>();
            services.AddSingleton<IDataOptions>(new DataOptions()
            {
                LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
            });
            // TokenManager that uses CryptAes with auto generated Key and IV.
            // Due to the auto-generation of the Key and IV, tokens are only valid during a single program execution.
            services.AddSingleton<ITokenManager, TokenManagerCrypt>();
            services.AddSingleton<IDataSource>(new DataSourceMain(new DataSourceConfig()));
            services.AddSingleton<IAuth, AuthMain>();
            services.AddSingleton<ICache, SimpleInMemoryCache>();
            services.AddSingleton<IUserIdentityService, DefaultUserIdentityService>();
            services.AddSingleton<IData, CoreMain>();
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
