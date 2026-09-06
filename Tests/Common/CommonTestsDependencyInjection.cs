using Microsoft.Extensions.DependencyInjection;
using Deve.Tests.Mocks.Repository;

namespace Deve.Tests;

public static class CommonTestsDependencyInjection
{
    public static IServiceCollection AddTests(this IServiceCollection services) =>
        services.AddSingleton(_ => TestsHelpers.CreateCrypt())
                .AddSingleton(_ => TestsHelpers.CreateHash())
                .AddSingleton(_ => TestsHelpers.CreateTokenManager())
                // <hooks:tests-di-repository-mock>
                .AddScoped(_ => new CountryRepositoryReadMock().Object)
                .AddScoped(_ => new CountryRepositoryWriteMock().Object)
                .AddScoped(_ => new StateRepositoryReadMock().Object)
                .AddScoped(_ => new StateRepositoryWriteMock().Object)
                .AddScoped(_ => new CityRepositoryReadMock().Object)
                .AddScoped(_ => new CityRepositoryWriteMock().Object)
                .AddScoped(_ => new ClientRepositoryReadMock().Object)
                .AddScoped(_ => new ClientRepositoryWriteMock().Object)
                .AddScoped(_ => new UserRepositoryReadMock().Object)
                .AddScoped(_ => new UserRepositoryWriteMock().Object);
}
