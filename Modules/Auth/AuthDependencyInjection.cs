using Microsoft.Extensions.DependencyInjection;

namespace Deve.Auth;

public static class AuthDependencyInjection
{
    public static IServiceCollection AddModuleAuth(this IServiceCollection services) => services.RegisterModule(typeof(Core).Assembly);

    public static IEndpointRouteBuilder MapEndpointsAuth(this IEndpointRouteBuilder builder) => builder.MapEndpoints(typeof(Core).Assembly);
}
