using Microsoft.Extensions.DependencyInjection;

namespace Deve.Auth;

public static class IdentityDependencyInjection
{
    public static IServiceCollection AddModuleIdentity(this IServiceCollection services) => services.RegisterModule(typeof(Core).Assembly);

    public static IEndpointRouteBuilder MapEndpointsIdentity(this IEndpointRouteBuilder builder) => builder.MapEndpoints(typeof(Core).Assembly);
}
