using Microsoft.Extensions.DependencyInjection;

namespace Deve.Identity;

public static class IdentityDependencyInjection
{
    public static IServiceCollection AddModuleIdentity(this IServiceCollection services) => services.RegisterModule(typeof(IdentityDependencyInjection).Assembly);

    public static IEndpointRouteBuilder MapEndpointsIdentity(this IEndpointRouteBuilder builder) => builder.MapEndpoints(typeof(IdentityDependencyInjection).Assembly);
}
