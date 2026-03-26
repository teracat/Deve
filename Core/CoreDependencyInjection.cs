using System.Reflection;
// <hooks:core-di-using>
using Deve.Auth;
using Deve.Auth.TokenManagers;
using Deve.Auth.UserIdentityService;
using Deve.Cache;
using Deve.Customers;
using Deve.Data;
using Deve.Identity;
using Deve.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Deve.Core;

public static class CoreDependencyInjection
{
    public static IServiceCollection AddCoreEmbedded(this IServiceCollection services, ILog log, IDataOptions options) =>
        services.AddCommon()
                .AddSingleton(log)
                // <hooks:core-di-addmodule>
                .AddModuleAuth()
                .AddModuleIdentity()
                .AddModuleCustomers()
                .AddSingleton(options)
                .AddSingleton<ICache, SimpleInMemoryCache>()
                // TokenManager that uses CryptAes with auto generated Key and IV.
                // Due to the auto-generation of the Key and IV, tokens are only valid during a single program execution.
                .AddSingleton<ITokenManager, TokenManagerCrypt>()
                .AddSingleton<IUserIdentityService, EmbeddedUserIdentityService>()
                .AddSingleton<IData, MainCore>();

    public static IServiceCollection AddConfiguration(this IServiceCollection services, Options.ConnectionStringsOptions options) =>
        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(options));

    public static IServiceCollection AddConfigurationAppSettings(this IServiceCollection services)
    {
        IConfiguration config = GetConfiguration();

        _ = services.Configure<Options.ConnectionStringsOptions>(config.GetSection("ConnectionStrings"));

        return services.AddSingleton(config);
    }

    private static IConfiguration GetConfiguration()
    {
        string path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? Directory.GetCurrentDirectory();
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return builder.Build();
    }
}
