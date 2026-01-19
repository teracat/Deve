using Microsoft.Extensions.DependencyInjection;
using Deve.Auth.UserIdentityService;
using Deve.Auth.TokenManagers;
using Deve.Cache;
using Deve.Data;
using Deve.Auth;
using Deve.Customers;
using Deve.Identity;

namespace Deve.Core;

public static class CoreDependencyInjection
{
    public static IServiceCollection AddCoreEmbedded(this IServiceCollection services, IDataOptions options) =>
        services.AddCommon()
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
