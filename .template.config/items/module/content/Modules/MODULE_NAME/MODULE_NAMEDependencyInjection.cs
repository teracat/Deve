using Microsoft.Extensions.DependencyInjection;

namespace Deve.MODULE_NAME;

public static class MODULE_NAMEDependencyInjection
{
    public static IServiceCollection AddModuleMODULE_NAME(this IServiceCollection services) => services.RegisterModule(typeof(MODULE_NAMEDependencyInjection).Assembly);

    public static IEndpointRouteBuilder MapEndpointsMODULE_NAME(this IEndpointRouteBuilder builder) => builder.MapEndpoints(typeof(MODULE_NAMEDependencyInjection).Assembly);
}
