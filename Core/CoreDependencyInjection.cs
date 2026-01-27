using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Deve.Api.Options;
// <hooks:core-di-using>
using Deve.Auth;
using Deve.Auth.TokenManagers;
using Deve.Auth.UserIdentityService;
using Deve.Cache;
using Deve.Customers;
using Deve.Data;
using Deve.Identity;

namespace Deve.Core;

public static class CoreDependencyInjection
{
    public static IServiceCollection AddCoreEmbedded(this IServiceCollection services, IDataOptions options)
    {
        IConfiguration config = GetConfiguration();

        _ = services.Configure<ConnectionStringsOptions>(config.GetSection("ConnectionStrings"));

        return services.AddCommon()
                .AddSingleton(config)
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
    }

    private static IConfiguration GetConfiguration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        _ = builder.AddJsonFile("appsettings.json");

        return builder.Build();
    }
}
