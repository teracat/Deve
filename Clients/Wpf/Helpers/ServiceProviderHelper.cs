using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Deve.Auth;
using Deve.Auth.Hash;
using Deve.Auth.TokenManagers;
using Deve.Auth.UserIdentityService;
using Deve.Cache;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Services;
using Deve.Clients.Wpf.ViewModels;
using Deve.Clients.Wpf.Views;
using Deve.Core;
using Deve.Data;
using Deve.DataSource;
using Deve.DataSource.Config;
using Deve.Internal.Data;

namespace Deve.Clients.Wpf.Helpers
{
    public static class ServiceProviderHelper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            _ = services.AddSingleton<INavigationService, NavigationService>();
            _ = services.AddSingleton<IMessageHandler, MessageHandlerMessageBox>();

            _ = services.AddSingleton<IHash, HashSha512>();
            _ = services.AddSingleton<IDataOptions>(new DataOptions()
            {
                LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
            });
            // TokenManager that uses CryptAes with auto generated Key and IV.
            // Due to the auto-generation of the Key and IV, tokens are only valid during a single program execution.
            _ = services.AddSingleton<ITokenManager, TokenManagerCrypt>();
            _ = services.AddSingleton<IDataSource>(new DataSourceMain(new DataSourceConfig()));
            _ = services.AddSingleton<IAuth, AuthMain>();
            _ = services.AddSingleton<ICache, SimpleInMemoryCache>();
            _ = services.AddSingleton<IUserIdentityService, EmbeddedUserIdentityService>();
            _ = services.AddSingleton<IData, CoreMain>();

            _ = services.AddSingleton<ISchedulerProvider, SchedulerProvider>();
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
}
