using Microsoft.Extensions.DependencyInjection;
using Deve.Tests.Mocks.Repository;

namespace Deve.Tests;

public static class CommonTestsDependencyInjection
{
    public static IServiceCollection AddTests(this IServiceCollection services) =>
        services.AddSingleton(_ => TestsHelpers.CreateCrypt())
                .AddSingleton(_ => TestsHelpers.CreateHash())
                .AddSingleton(_ => TestsHelpers.CreateTokenManager())
                .AddScoped(_ => new CountryRepositoryMock().Object)
                .AddScoped(_ => new StateRepositoryMock().Object)
                .AddScoped(_ => new CityRepositoryMock().Object)
                .AddScoped(_ => new ClientRepositoryMock().Object)
                .AddScoped(_ => new UserRepositoryMock().Object);
}
