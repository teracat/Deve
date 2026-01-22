using Microsoft.Extensions.DependencyInjection;
using Deve.Core;
using Deve.Data;
using Deve.Auth;
// <hooks:tests-core-main-using>
using Deve.Customers;
using Deve.Identity;
using Deve.Identity.Enums;
using Deve.Tests.Mocks.IdentityService;

namespace Deve.Tests.Core;

public sealed class MainTestsCore : IData
{
    private readonly ServiceProvider _serviceProvider;

    // <hooks:tests-core-main-properties>

    public IAuthData Auth => _serviceProvider.GetRequiredService<IAuthData>();

    public ICustomersData Customers => _serviceProvider.GetRequiredService<ICustomersData>();

    public IIdentityData Identity => _serviceProvider.GetRequiredService<IIdentityData>();

    public MainTestsCore(IDataOptions options, Role? role)
    {
        var services = new ServiceCollection();
        _ = services.AddCoreEmbedded(options);
        _ = services.AddTests();

        if (role is not null)
        {
            _ = role.Value switch
            {
                Role.Admin => services.AddSingleton(_ => new AdminAuthUserIdentityServiceMock().Object),
                Role.User => services.AddSingleton(_ => new UserAuthUserIdentityServiceMock().Object),
                _ => throw new NotSupportedException($"Role '{role.Value}' is not supported in tests."),
            };
        }

        _serviceProvider = services.BuildServiceProvider();
    }

    public void Dispose() => _serviceProvider.Dispose();
}
